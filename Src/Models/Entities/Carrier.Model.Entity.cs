using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("fornecedor")]
   public class CarrierModel
   {
      private static CarrierModel FInstancia { get; set; }

      public static CarrierModel New()
      {
         FInstancia ??= new CarrierModel();
         return FInstancia;
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [JsonPropertyName("pessoa_id")]
      [Column("pessoa_id")]
      public int PessoaId { get; set; }

      [JsonIgnore]
      [Required(ErrorMessage = "Propriedade {0} é obrigatória")]
      public virtual PersonModel Person { get; set; }
   }
}
