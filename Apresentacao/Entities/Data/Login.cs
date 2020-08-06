using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apresentacao.Entities
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [NotMapped]
        [Required]
        public string Password { get; set; }
        public byte[] PasswordCrypto { get; set; }
    }
}
