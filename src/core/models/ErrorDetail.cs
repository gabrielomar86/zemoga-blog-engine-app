using System.Text.Json;

namespace core.models
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public string TargetSite { get; set; }

        public string Source { get; set; }

        public string Detail { get; set; }

        public string InnerException { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, null, new JsonSerializerOptions { IgnoreNullValues = true });
        }

        public ErrorDetail RemoveDetail()
        {
            Detail = null;
            return this;
        }
    }
}
