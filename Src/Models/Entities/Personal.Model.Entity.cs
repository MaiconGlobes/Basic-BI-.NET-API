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

      [JsonPropertyName("apelido")]
      [Column("apelido", TypeName = "varchar")]
      [MaxLength(35, ErrorMessage = "Propriedade {0} deve ter no máximo 35 caracteres")]
      public string Apelido { get; set; }

      [JsonPropertyName("cpf_cnpj")]
      [Column("cpf_cnpj", TypeName = "varchar")]
      [MinLength(11, ErrorMessage = "Propriedade {0} deve ter no mínimo 11 caracteres")]
      [MaxLength(14, ErrorMessage = "Propriedade {0} deve ter no máximo 14 caracteres")]
      public string Cpf_cnpj { get; set; }

      [JsonPropertyName("telefone")]
      [Column("telefone", TypeName = "varchar")]
      [MaxLength(11, ErrorMessage = "Propriedade {0} deve ter no máximo 11 caracteres")]
      public string Telefone { get; set; }

      public virtual ICollection<UserModel> Users { get; set; }
      public virtual ICollection<AddressModel> Addresses { get; set; }
      public virtual ClientModel Client { get; set; }
      public virtual CarrierModel Carrier { get; set; }
   }
}
