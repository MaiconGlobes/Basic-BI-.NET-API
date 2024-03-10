using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   public class UserModel : PersonalModel
   {
      private static UserModel FInstancia { get; set; }

      public static UserModel New()
      {
         FInstancia ??= new UserModel();
         return FInstancia;
      }

      [JsonPropertyName("idade")]
      [Column("idade")]
      public byte Idade { get; set; }

      [JsonPropertyName("senha")]
      [Column("senha", TypeName = "varchar")]
      [MaxLength(255, ErrorMessage = "Propriedade {0} deve ter no máximo 255 caracteres")]
      public string Senha { get; set; }
   }
}
