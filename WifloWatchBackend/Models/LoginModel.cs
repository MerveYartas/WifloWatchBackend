using System.ComponentModel.DataAnnotations;

namespace WifloWatchBackend.Models
{
    public class LoginModel
    {
        [Key]  // Burada birincil anahtar olarak kullanacağınız özelliği işaret ediyorsunuz
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
