using System.ComponentModel.DataAnnotations;

namespace Hospital_management.CustomModels
{
    public class SigninUser
    {
        [Required(ErrorMessage = "İstifadəçi adı boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "İstifadəçi adında boşluq buraxmaq olmaz")]
        [StringLength(25, ErrorMessage = "Minimum 3, maksimum 25 simvol ola bilər", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifrədə boşluq buraxmaq olmaz")]
        [StringLength(25, ErrorMessage = "Minimum 8, maksimum 25 simvol ola bilər", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
