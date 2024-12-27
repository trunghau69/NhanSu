using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class PhongBan
    {
        public int ID { get; set; }
        [DisplayName("Mả Phòng Ban")]
        public string MaPhongBan { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên Phòng Không Được Bỏ Trống!")]
        [DisplayName("Tên Phòng Ban")]
        public string TenPhongBan { get; set; }
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}
