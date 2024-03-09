using BaseCodeAPI.Src.Models.Entity;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models
{
    public class ResultJSONModelDto
    {
        [JsonPropertyName("parametros")]
        public ParametroModel parametros { get; set; }
    }
}
