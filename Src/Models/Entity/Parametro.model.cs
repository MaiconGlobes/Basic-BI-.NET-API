using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
    public class ParametroModel
    {
        private static ParametroModel FInstancia { get; set; }

        public static ParametroModel New()
        {
            FInstancia ??= new ParametroModel();
            return FInstancia;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", Order = 1)]
        public int Id { get; set; }

        [JsonPropertyName("telefone_suporte")]
        [Column("telefone_suporte", TypeName = "text", Order = 2)]
        [Required]
        [MaxLength(11)]
        public string Telefone_suporte { get; set; }
    }
}
