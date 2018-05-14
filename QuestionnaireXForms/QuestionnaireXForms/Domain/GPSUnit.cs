
using Newtonsoft.Json;

namespace QuestionnaireXForms.Domain
{
    public class GPSUnit
    {
        public long Id { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Plate { get; set; }

        public string DisplayName => Plate ?? Name;
    }
}
