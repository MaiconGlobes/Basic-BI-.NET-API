using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseCodeAPI.Src.Models.Entity
{
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
        [Column("id", Order = 1)]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Column("name", TypeName = "text", Order = 2)]
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
    }
}
