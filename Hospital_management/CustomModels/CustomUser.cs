using System.ComponentModel.DataAnnotations;

namespace Hospital_management.CustomModels
{
    public class CustomUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad boş ola bilməz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad boş ola bilməz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Nömrə boş ola bilməz")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "İstifadəçi adı boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "İstifadəçi adında boşluq buraxmaq olmaz")]
        [StringLength(25, ErrorMessage = "Minimum 3, maksimum 25 simvol ola bilər", MinimumLength = 3)]
        public string UserName { get; set; }
        public int? StatusId { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifrədə boşluq buraxmaq olmaz")]
        [StringLength(25, ErrorMessage = "Minimum 8, maksimum 25 simvol ola bilər", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrə təsdiq boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifrədə boşluq buraxmaq olmaz")]
        [Compare("Password", ErrorMessage = "Şifrə və təsdiq şifrəsi eyni olmalıdır")]
        [StringLength(25, ErrorMessage = "Minimum 8, maksimum 25 simvol ola bilər", MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}
