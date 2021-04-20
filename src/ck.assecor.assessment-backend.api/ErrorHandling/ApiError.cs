namespace ck.assecor.assessment_backend.api.ErrorHandling
{
	/// <summary>
	/// Errorwrapper to wrap exceptions and give back api error codes and descriptions
	/// </summary>
	public class ApiError
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApiError"/> class.
		/// </summary>
		public ApiError()
		{
			this.Description = string.Empty;
		}

		/// <summary>
		/// Gets or sets the error code.
		/// </summary>
		/// <value>
		/// The error code.
		/// </value>
		public int Code { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string Description { get; set; }
	}
}
