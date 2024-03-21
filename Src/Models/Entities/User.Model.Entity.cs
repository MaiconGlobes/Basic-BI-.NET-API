using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("usuario")]
   public class UserModel
   {
      private static UserModel FInstancia { get; set; }

      public static UserModel New()
      {
         FInstancia ??= new UserModel();
         return FInstancia;
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [JsonPropertyName("login")]
      [Column("login", TypeName = "varchar")]
      [MaxLength(15)]
      public string Login { get; set; }

      [Column("email", TypeName = "varchar")]
      [MaxLength(255)]
      public string Email { get; set; }

      [Column("senha", TypeName = "varchar")]
      [MaxLength(100)]
      public string Senha { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Column("refresh_token", TypeName = "text")]
      public string Refresh_token { get; set; }

      [Column("pessoa_id")]
      public int PessoaId { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      public virtual PersonModel Person { get; set; }

   }
}
