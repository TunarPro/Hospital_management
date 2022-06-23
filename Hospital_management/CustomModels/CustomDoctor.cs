using System.ComponentModel.DataAnnotations;

namespace Hospital_management.CustomModels
{
    public class CustomDoctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad boş ola bilməz")]
        [StringLength(25, ErrorMessage = "Maksimum 25 simvol ola bilər")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad boş ola bilməz")]
        [StringLength(25, ErrorMessage = "Maksimum 25 simvol ola bilər")]
        public string Surname { get; set; }

        [Range(0, 100, ErrorMessage = "Maksimum 100 ola bilər")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Yaş 0 ilə başlaya bilməz")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Vəzifə boş ola bilməz")]
        [StringLength(55, ErrorMessage = "Maksimum 55 simvol ola bilər")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Təcrübə boş ola bilməz")]
        [StringLength(25, ErrorMessage = "Maksimum 25 simvol ola bilər")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "İstifadəçi adı boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "İstifadəçi adında boşluq buraxmaq olmaz")]
        [StringLength(25, ErrorMessage = "Minimum 3, maksimum 25 simvol ola bilər", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifrədə boşluq buraxmaq olmaz")]
        [StringLength(25, ErrorMessage = "Minimum 8, maksimum 25 simvol ola bilər", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifrə təsdiq boş ola bilməz")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Şifrədə boşluq buraxmaq olmaz")]
        [Compare("Password", ErrorMessage = "Şifrə və təsdiq şifrəsi eyni olmalıdır")]
        [StringLength(25, ErrorMessage = "Minimum 8, maksimum 25 simvol ola bilər", MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
        public int? AppointmentCount { get; set; }
    }
}
