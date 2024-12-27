using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class ChuyenNganh
    {
        public int ID { get; set; }
        [DisplayName("Mã Chuyên Ngành")]
        public string MaChuyenNganh { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên Chuyên Ngành Không Được Bỏ Trống")]
        [DisplayName("Tên Chuyên Ngành")]
        public string TenChuyenNganh { get; set; }
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}
