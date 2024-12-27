using Employee.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class ChamCong
    {
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Thời Gian Làm Việc ID")]
        [Required(ErrorMessage = "Thời Gian Làm Việc ID không được bỏ trống!")]
        public int ThoiGianLamViecID { get; set; }

        [DisplayName("Ngày Chấm Công")]
        [Required(ErrorMessage = "Ngày không được bỏ trống!")]
        public DateTime Ngay { get; set; }

        [DisplayName("Trạng Thái")]
        [Required(ErrorMessage = "Trạng thái không được bỏ trống!")]
        public string TrangThai { get; set; } = string.Empty;

        [DisplayName("Số Giờ Làm")]
        [Range(0, 24, ErrorMessage = "Số giờ làm phải nằm trong khoảng từ 0 đến 24!")]
        public decimal SoGioLam { get; set; }

        [DisplayName("Ghi Chú")]
        public string? GhiChu { get; set; }

        // Quan hệ với bảng ThoiGianLamViec
        public ThoiGianLamViec? ThoiGianLamViec { get; set; }
    }
}
