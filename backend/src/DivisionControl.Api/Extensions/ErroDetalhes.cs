using System.Text.Json;

namespace DivisionControl.Api.Extensions
{
    public class ErroDetalhes
    {
        public int Status { get; set; }
        public string Message { get; set; } = null;
        public Exception Exception { get; set; } = null;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
