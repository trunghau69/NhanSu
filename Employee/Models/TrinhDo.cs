using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class TrinhDo
    {
        public int ID { get; set; }
        [DisplayName("Mã Trình Độ")]
        public string MaTrinhDo { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên Trình Độ Không Được Bỏ Trống!")]
        [DisplayName("Tên Trình Độ")]
        public string TenTrinhDo { get; set; }
        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}
