using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Employee.Models
{
    public class CoSoDaoTao
    {
        public int ID { get; set; }
        [DisplayName("Mã Cơ Sở Đào Tạo")]
        public string MaCoSo { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên Chức Vụ Không Được Bỏ Trống!")]
        [DisplayName("Tên Chức vụ")]
        public string TenCoSo { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}
