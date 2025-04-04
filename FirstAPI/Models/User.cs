using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }=string.Empty;
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }

        public Employee? Employee { get; set; }
    }
}
