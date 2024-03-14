using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class PersonModelDTO
   {
      [JsonPropertyName("nome")]
      [MaxLength(50, ErrorMessage = "Propriedade {0} deve ter no máximo 50 caracteres")]
      public string Nome { get; set; }

      [JsonPropertyName("cpf_cnpj")]
      [MinLength(11, ErrorMessage = "Propriedade {0} deve ter no mínimo 11 caracteres")]
      [MaxLength(14, ErrorMessage = "Propriedade {0} deve ter no máximo 14 caracteres")]
      public string Cpf_cnpj { get; set; }

      [JsonPropertyName("telefone")]
      [MinLength(10, ErrorMessage = "Propriedade {0} deve ter no mínimo 10 caracteres")]
      [MaxLength(11, ErrorMessage = "Propriedade {0} deve ter no máximo 11 caracteres")]
      public string Telefone { get; set; }
   }
}
