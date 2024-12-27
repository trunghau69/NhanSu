using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Models
{
    public class NhanVien
    {
        public int ID { get; set; }

        [DisplayName("Mã Nhân Viên")]
        [Required(ErrorMessage = "Mã Nhân Viên Không Được Bỏ Trống!")]
        public string MaNhanVien { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Tên Nhân Viên Không Được Bỏ Trống!")]
        [DisplayName("Tên Nhân Viên")]
        public string TenNhanVien { get; set; }

        [StringLength(255)]
        [DisplayName("Hình Ảnh")]
        public string? HinhAnh { get; set; }  

        [NotMapped]
        [Display(Name = "Hình ảnh nhân viên")]
        public IFormFile? HinhAnhNhanVien { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Giới Tính Không Được Bỏ Trống!")]
        [DisplayName("Giới Tính")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Ngày Sinh Không Được Bỏ Trống!")]
        [DisplayName("Ngày Sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgaySinh { get; set; }

        [StringLength(11, ErrorMessage = "Số điện thoại không được vượt quá 11 ký tự.")]
        [Required(ErrorMessage = "Điện thoại không được bỏ trống!")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại phải là 10 hoặc 11 chữ số.")]
        [DisplayName("Điện thoại")]
        public string? SDT { get; set; }

        [RegularExpression(@"^\d{12}$", ErrorMessage = "Căn Cước Công Dân Phải Là 12 Chữ Số")]
        [DisplayName("CCCD")]
        public string CanCuocCongDan { get; set; }

        [StringLength(100)]
        [DisplayName("Email")]
        public string Email { get; set; }

        // Optional Fields (can be left empty)
        [StringLength(100)]
        [DisplayName("Quốc Tịch")]
        public string? QuocTich { get; set; }

        [StringLength(100)]
        [DisplayName("Dân Tộc")]
        public string? DanToc { get; set; }

        [StringLength(255)]
        [DisplayName("Nơi Ở")]
        public string? NoiO { get; set; }

        [StringLength(255)]
        [DisplayName("Quê Quán")]
        public string? QueQuan { get; set; }

        [Required(ErrorMessage = "Chức Vụ Không Được Bỏ Trống!")]
        [DisplayName("Chức Vụ")]
        public int ChucVuID { get; set; }
        [Required(ErrorMessage = "Chuyên Môn Không Được Bỏ Trống!")]
        [DisplayName("Chuyên Môn")]
        public int ChuyenNganhID { get; set; }


        [Required(ErrorMessage = "Phòng Ban Không Được Bỏ Trống!")]
        [DisplayName("Phòng Ban")]
        public int PhongBanId { get; set; }


        [Required(ErrorMessage = "Trình Độ Không Được Bỏ Trống!")]
        [DisplayName("Trình Độ")]
        public int TrinhDoID { get; set; }

        public ICollection<Luong>? Luong { get; set; }
        public ICollection<NguoiDung>? NguoiDung { get; set; }

        public ICollection<NghiPhep>? NghiPhep { get; set; }


        [DisplayName("Chức Vụ")]
        public ChucVu? ChucVu { get; set; }
        [DisplayName("Chuyên Ngành")]
        public ChuyenNganh? ChuyenNganh { get; set; }
        [DisplayName("Phòng Ban")]
        public PhongBan? PhongBan { get; set; }
        [DisplayName("Trình Độ")]
        public TrinhDo? TrinhDo { get; set; }
    }

}
