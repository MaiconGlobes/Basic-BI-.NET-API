using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class ClientModel : PersonalModel
   {
      private static ClientModel FInstancia { get; set; }

      public static ClientModel New()
      {
         FInstancia ??= new ClientModel();
         return FInstancia;
      }

      [JsonPropertyName("cpf_cnpj")]
      [Column("cpf_cnpj", TypeName = "varchar")]
      [MinLength(11, ErrorMessage = "Propriedade {0} deve ter no mínimo 11 caracteres")]
      [MaxLength(14, ErrorMessage = "Propriedade {0} deve ter no máximo 14 caracteres")]
      public string Cpf_cnpj { get; set; }

      [JsonPropertyName("limite_credito")]
      [Column("limite_credito", TypeName = "numeric(18,2)")]
      public double Limite_credito { get; set; }
   }
}
