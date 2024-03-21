using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class UserModelDto
   {
      [JsonIgnoreAttribute]
      public int Id { get; set; }

      [JsonPropertyName("login")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(15, ErrorMessage = "Propriedade {0} deve ter no máximo 15 caracteres")]
      public string Login { get; set; }

      [JsonPropertyName("email")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(255, ErrorMessage = "Propriedade {0} deve ter no máximo 255 caracteres")]
      public string Email { get; set; }

      [JsonPropertyName("senha")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Senha { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Column("refresh_token", TypeName = "text")]
      public string Refresh_token { get; set; }

      [JsonPropertyName("token")]
      public string Token { get; set; }

      [JsonPropertyName("pessoa_id")]
      public int PessoaId { get; set; }

      public PersonModelDTO Pessoa { get; set; }
   }
}
