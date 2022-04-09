using Newtonsoft.Json;

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
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public ErrorDetail RemoveDetail()
        {
            Detail = null;
            return this;
        }
    }
}
