using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Models
{
    public class ChucVu
    {
        public int ID { get; set; }
        [DisplayName("Mã Chức Vụ")]
        public string MaChucVu { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Tên Chức Vụ Không Được Bỏ Trống!")]
        [DisplayName("Tên Chức vụ")]
        public string TenChucVu { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô Tả Chức Vụ")]
        [DataType(DataType.MultilineText)]
        public string? MoTa { get; set; }

        public ICollection<NhanVien>? NhanVien { get; set; }
    }
}
