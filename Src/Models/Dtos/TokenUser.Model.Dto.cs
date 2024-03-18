using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class TokenUserModelDto
   {
      [JsonPropertyName("token")]
      public string Token { get; set; }

      public static explicit operator TokenUserModelDto(Stream v)
      {
         throw new NotImplementedException();
      }
   }
}
