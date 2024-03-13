using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class PersonModelDTO
   {
      [JsonPropertyName("apelido")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(35, ErrorMessage = "Propriedade {0} deve ter no máximo 35 caracteres")]
      public string Apelido { get; set; }

      [JsonPropertyName("cpf_cnpj")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MinLength(11, ErrorMessage = "Propriedade {0} deve ter no mínimo 11 caracteres")]
      [MaxLength(14, ErrorMessage = "Propriedade {0} deve ter no máximo 14 caracteres")]
      public string Cpf_cnpj { get; set; }

      [JsonPropertyName("telefone")]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      [MaxLength(11, ErrorMessage = "Propriedade {0} deve ter no máximo 11 caracteres")]
      public string Telefone { get; set; }
   }
}
