using System.ComponentModel.DataAnnotations;

namespace ECommerceManager.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(100, ErrorMessage = "Şifre en az {2} karakter olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
    }
}
