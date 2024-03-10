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

      [JsonPropertyName("reference")]
      [Column("reference")]
      [Required]
      public Guid Reference { get; set; }

      [JsonPropertyName("idade")]
      [Column("idade")]
      [Required]
      [MaxLength(35)]
      public byte Idade { get; set; }

      [JsonPropertyName("senha")]
      [Column("senha", TypeName = "varchar")]
      [Required]
      [MaxLength(255)]
      public string Senha { get; set; }
}
}
