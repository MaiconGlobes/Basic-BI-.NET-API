using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class UserModel : PersonModel
   {
      private static UserModel FInstancia { get; set; }

      public static UserModel New()
      {
         FInstancia ??= new UserModel();
         return FInstancia;
      }

      [JsonPropertyName("email")]
      [Column("email", TypeName = "varchar")]
      [MaxLength(255, ErrorMessage = "Propriedade {0} deve ter no máximo 255 caracteres")]
      public string Email { get; set; }

      [JsonPropertyName("senha")]
      [Column("senha", TypeName = "varchar")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Senha { get; set; }
   }
}
