using System.Text.Json.Serialization;

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
