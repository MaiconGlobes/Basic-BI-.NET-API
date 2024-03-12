using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
    public class ClientModel 
    {
        private static ClientModel FInstancia { get; set; }

        public static ClientModel New()
        {
            FInstancia ??= new ClientModel();
            return FInstancia;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [JsonPropertyName("limite_credito")]
        [Column("limite_credito", TypeName = "numeric(18,2)")]
        public double Limite_credito { get; set; }

        [JsonPropertyName("pessoa_id")]
        [Column("pessoa_id")]
        public int PessoaId { get; set; }

        public PersonModel Person { get; set; }
    }
}
