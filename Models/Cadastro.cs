using System.ComponentModel.DataAnnotations;

namespace CadastroApi.Models
{
    public class Cadastro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
