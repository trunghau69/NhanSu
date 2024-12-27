using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Models
{
    public class HopDong
    {
        public int ID { get; set; }
        [DisplayName("Mã Hợp Đồng")]
        public string MaHopDong { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên Hợp Đồng Không Được Bỏ Trống!")]
        [DisplayName("Tên Hợp Đồng")]
        public string TenHopDong { get; set; }
        [Required(ErrorMessage = "Ngày Bắt Đầu Không Được Bỏ Trống!")]
        [DisplayName("Ngày Bắt Đầu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Ngày Kết Thúc Không Được Bỏ Trống!")]
        [DisplayName("Ngày Kết Thúc")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayKetThuc { get; set; }
        [Column(TypeName = "ntext")]
        [DisplayName("Nội Dung Hợp Đồng")]
        [DataType(DataType.MultilineText)]
        public string? NoiDung { get; set; }
        [Required(ErrorMessage = "Ngày Ký Không Được Bỏ Trống!")]
        [DisplayName("Ngày Ký Hợp Đồng")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayKy { get; set; }
        [DisplayName("Tên Nhân Viên")]
        public int NhanVienID { get; set; }

        public NhanVien? NhanVien { get; set; }
    }
}
