using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
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

        [JsonPropertyName("cnpj")]
        [Column("cnpj", TypeName = "varchar")]
        [MinLength(14, ErrorMessage = "Propriedade {0} deve ter no mínimo 11 caracteres")]
        [MaxLength(14, ErrorMessage = "Propriedade {0} deve ter no máximo 14 caracteres")]
        public string Cnpj { get; set; }


        [JsonPropertyName("pessoa_id")]
        [Column("pessoa_id")]
        public int PessoaId { get; set; }

        public PersonModel Person { get; set; }
    }
}
