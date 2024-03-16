using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("pessoa")]
   public class PersonModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [JsonPropertyName("nome")]
      [Column("nome", TypeName = "varchar")]
      [MaxLength(50)]
      public string Nome { get; set; }

      [JsonPropertyName("cpf_cnpj")]
      [Column("cpf_cnpj", TypeName = "varchar")]
      [MinLength(11)]
      [MaxLength(14)]
      public string Cpf_cnpj { get; set; }

      [JsonPropertyName("telefone")]
      [Column("telefone", TypeName = "varchar")]
      [MinLength(10)]
      [MaxLength(11)]
      public string Telefone { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual UserModel User { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual ClientModel Client { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual CarrierModel Carrier { get; set; }

      [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
      public virtual TransporterModel Transporter { get; set; }
      public virtual ICollection<AddressModel> Addresses { get; set; }
   }
}
