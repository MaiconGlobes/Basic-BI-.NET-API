using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class UserModelDto
   {
      [JsonPropertyName("apelido")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(35, ErrorMessage = "Propriedade {0} deve ter no máximo 35 caracteres")]
      public string Apelido { get; set; }

      [JsonPropertyName("email")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(255, ErrorMessage = "Propriedade {0} deve ter no máximo 255 caracteres")]
      public string Email { get; set; }

      [JsonPropertyName("senha")]
      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Senha { get; set; }

      [JsonPropertyName("pessoa_id")]
      public int PessoaId { get; set; }

      public PersonModelDTO Pessoa { get; set; }
   }
}
