using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage ="Username is required")]
        public String? UserName{get;init;}  // classı record olarak tanımladığımız için veriler tanımlandığı an verilmeli bu yüzden init olarak güncelliyoruz
        [Required(ErrorMessage ="Email is required")]
        public String? Email{get;init;}
        [Required(ErrorMessage ="Password is required")]
        public String? Password{get;init;}
    }
}