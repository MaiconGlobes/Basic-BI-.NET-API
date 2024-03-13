using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
   [Table("endereco")]
   public class AddressModel
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [Column("id")]
      public int Id { get; set; }

      [JsonPropertyName("endereco")]
      [Column("endereco", TypeName = "varchar")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Endereco { get; set; }

      [JsonPropertyName("numero")]
      [Column("numero", TypeName = "varchar")]
      [MaxLength(10, ErrorMessage = "Propriedade {0} deve ter no máximo 10 caracteres")]
      public string Numero { get; set; }

      [JsonPropertyName("bairro")]
      [Column("bairro", TypeName = "varchar")]
      [MaxLength(35, ErrorMessage = "Propriedade {0} deve ter no máximo 35 caracteres")]
      public string Bairro { get; set; }

      [JsonPropertyName("municipio")]
      [Column("municipio", TypeName = "varchar")]
      [MaxLength(100, ErrorMessage = "Propriedade {0} deve ter no máximo 100 caracteres")]
      public string Municipio { get; set; }

      [JsonPropertyName("uf")]
      [Column("uf", TypeName = "varchar")]
      [MaxLength(2, ErrorMessage = "Propriedade {0} deve ter no máximo 2 caracteres")]
      public string Uf { get; set; }

      [JsonPropertyName("cep")]
      [Column("cep", TypeName = "varchar")]
      [MaxLength(8, ErrorMessage = "Propriedade {0} deve ter no máximo 8 caracteres")]
      public string Cep { get; set; }

      [JsonPropertyName("pessoa_id")]
      [Column("pessoa_id")]
      public int PessoaId { get; set; }

      [JsonIgnore]
      public virtual PersonModel Person { get; set; }
   }
}
