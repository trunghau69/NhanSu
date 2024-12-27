using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Employee.Models
{
    public class NghiPhep
    {
        public int ID { get; set; }

        [DisplayName("Mã Nhân Viên")]
        [Required(ErrorMessage = "Mã Nhân Viên Không Được Bỏ Trống!")]
        public int NhanVienID { get; set; }
        [Required(ErrorMessage = "Ngày Bắt Đầu Không Được Bỏ Trống!")]
        [DisplayName("Ngày Bắt Đầu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayBatDau { get; set; }
        [Required(ErrorMessage = "Ngày Kết Thúc Không Được Bỏ Trống!")]
        [DisplayName("Ngày Kết Thúc")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayKetThuc { get; set; }

        [DisplayName("Số Ngày")]
        public int SoNgay { get; set; }
        [Required(ErrorMessage = "Lý Do Không Được Bỏ Trống!")]
        [DisplayName("Lý Do")]
        public string LyDo { get; set; }
        [DisplayName("Trạng Thái")]
        public TrangThaiNghiPhep TrangThai { get; set; }

        [DisplayName("Nhân Viên")]
        public NhanVien? NhanVien { get; set; }

    }
    public enum TrangThaiNghiPhep
    {
        [Display(Name = "Chờ Duyệt")]
        ChoDuyet,
        [Display(Name = "Đã Duyệt")]
        DaDuyet,
        [Display(Name = "Từ Chối")]
        TuChoi,
    }
}
