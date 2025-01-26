using System.ComponentModel.DataAnnotations;

namespace CadastroApi.Models
{
    public class Cadastro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public required string CPF { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
