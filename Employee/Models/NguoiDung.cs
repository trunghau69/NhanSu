using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Models
{
    public class NguoiDung
    {
            public int ID { get; set; }

            [DisplayName("Họ Tên Nhân Viên")]
            public int NhanVienID { get; set; }

            [StringLength(100)]
            [Required(ErrorMessage = "Email không được bỏ trống!")]
            [DisplayName("Email")]
            public string EmailID { get; set; }

            [StringLength(50, ErrorMessage = "{0} phải ít nhất {2} ký tự.", MinimumLength = 4)]
            [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
            [DisplayName("Tên Đăng Nhập")]
            public string TenDangNhap { get; set; }

            [StringLength(255)]
            [DataType(DataType.Password)]
            [DisplayName("Mật khẩu")]
            public string? MatKhau { get; set; }

            [NotMapped]
            [DisplayName("Xác nhận mật khẩu")]
            [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
            [DataType(DataType.Password)]
            public string? XacNhanMatKhau { get; set; }

            public enum QuyenHan
            {
                Admin,
                [Display(Name="Nhân Viên")]
                NhanVien
            }
            [DisplayName("Quyền")]
            public QuyenHan Quyen { get; set; }
            public NhanVien? NhanVien { get; set; }
    }
    [NotMapped]
    public class DangNhap
    {
        [StringLength(50, ErrorMessage = "{0} Phải Ít Nhất {2} Ký Tự.", MinimumLength = 4)]
        [Required(ErrorMessage = "Tên Đăng Nhập Không Được Bỏ Trống!")]
        [DisplayName("Tên Đăng Nhập")]
        public string TenDangNhap { get; set; }
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }
        [DisplayName("Duy trì đăng nhập")]
        public bool DuyTriDangNhap { get; set; }
        [StringLength(255)]
        [DisplayName("Liên kết chuyển trang")]
        public string? LienKetChuyenTrang { get; set; }
    }
}

