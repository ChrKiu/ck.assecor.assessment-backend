namespace ck.assecor.assessment_backend.data.CsvContext
{
    /// <summary>
    /// Required configurations for accessing the CSV-Data-Source
    /// </summary>
    public interface ICsvConfiguration
    {
        string Path { get; }
    }
}
