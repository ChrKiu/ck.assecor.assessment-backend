using System;
using System.Net;
using System.Threading.Tasks;
using ck.assecor.assessment_backend.data.Exceptions;
using ck.assecor.assessment_backend.infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ck.assecor.assessment_backend.api.ErrorHandling
{
	/// <summary>
	/// Middleware which catches exceptions and returns an <see cref="ApiError"/> instead
	/// </summary>
	public sealed class ExceptionMiddleware
	{
		private static readonly JsonSerializerSettings serializerSettings =
			new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				Formatting = Formatting.Indented,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

		private readonly ILogger<ExceptionMiddleware> logger;

		private readonly RequestDelegate next;

		/// <summary>
		///     Initializes a new instance of the <see cref="ExceptionMiddleware" /> class.
		/// </summary>
		/// <param name="next">The next.</param>
		/// <param name="loggerFactory">The logger factory.</param>
		public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			this.next = next;
			this.logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
		}

		/// <summary>
		///     Invokes the asynchronous.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await this.next(context);
			}
			catch (Exception exception)
			{
				await this.HandleExceptionAsync(context, exception);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			this.logger.LogError(exception, exception.Message);
			var apiError = new ApiError();

			if (exception is InvalidSearchParameterException)
			{
				
				apiError.Description = exception.GetBaseException().Message;
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			else if (exception is CsvDataSourceException)
			{
				apiError.Description = exception.GetBaseException().Message;
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
			else if (exception is ArgumentNullException)
			{
				apiError.Description = exception.GetBaseException().Message;
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
			else
			{
				apiError.Description = "Unknown error";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}

			apiError.Code = context.Response.StatusCode;

			var serializeObject = JsonConvert.SerializeObject(apiError, serializerSettings);
			context.Response.ContentType = "application/json";
			return context.Response.WriteAsync(serializeObject);
		}
	}
}
