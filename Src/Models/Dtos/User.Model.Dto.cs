using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class UserModelDto
   {
      [JsonPropertyName("email")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(255, ErrorMessage = "Propriedade {0} deve ter no máximo 255 caracteres")]
      public string Email { get; set; }

      [JsonPropertyName("senha")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Senha { get; set; }

      [JsonPropertyName("pessoa_id")]
      public int PessoaId { get; set; }

      public PersonModelDTO Pessoa { get; set; }
   }
}
