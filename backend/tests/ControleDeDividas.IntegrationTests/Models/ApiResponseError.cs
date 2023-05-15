namespace ControleDeDividas.IntegrationTests.Models
{
    public class ApiResponseError
    {
        public bool suceess { get; set; }
        public int status { get; set; }
        public List<string> errors { get; set; }
    }
}
