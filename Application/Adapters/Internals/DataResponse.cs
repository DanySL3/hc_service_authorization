using Newtonsoft.Json;

namespace Application.Adapters.Internals
{
    public class DataResponse
    {
        [JsonIgnore]
        public int ResponseCode { get; set; } = 0;

        public int Success { get; set; } = 0;

        public string Message { get; set; } = "";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Errors { get; set; } = new List<object>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { set; get; } = new { };
    }
}
