using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Employee.Models;

namespace Employee.Models
{
    public class ThoiGianLamViec
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Mã Nhân Viên")]
        [Required(ErrorMessage = "Mã Nhân Viên không được bỏ trống!")]
        public int NhanVienID { get; set; }

        [Required(ErrorMessage = "Tháng không được bỏ trống!")]
        [Range(1, 12, ErrorMessage = "Tháng phải nằm trong khoảng từ 1 đến 12!")]
        [DisplayName("Tháng")]
        public int Thang { get; set; }

        [Required(ErrorMessage = "Năm không được bỏ trống!")]
        [Range(2000, 2100, ErrorMessage = "Năm không hợp lệ!")]
        [DisplayName("Năm")]
        public int Nam { get; set; }

        [Required(ErrorMessage = "Số ngày nghỉ không được bỏ trống!")]
        [Range(0, 31, ErrorMessage = "Số ngày nghỉ phải nằm trong khoảng từ 0 đến 31!")]
        [DisplayName("Số Ngày Nghỉ")]
        public int SoNgayNghi { get; set; }

        [Required(ErrorMessage = "Số ngày công không được bỏ trống!")]
        [Range(0, 31, ErrorMessage = "Số ngày công phải nằm trong khoảng từ 0 đến 31!")]
        [DisplayName("Số Ngày Công")]
        public int SoNgayCong { get; set; }

        // Quan hệ với bảng NhanVien
        [DisplayName("Nhân Viên")]
        public NhanVien? NhanVien { get; set; }

        // Quan hệ với bảng NghiPhep
        public ICollection<NghiPhep>? NghiPhep { get; set; }

        // Quan hệ với bảng Luong
        public ICollection<Luong>? Luong { get; set; }
    }
}
