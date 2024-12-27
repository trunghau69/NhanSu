using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class KyLuat
    {
        public int ID { get; set; }
        [DisplayName("Mã Kỷ Luật")]
        public string MaKyLuat { get; set; }
        [StringLength(255)]
        [Required(ErrorMessage = "Lý Do Không Được Bỏ Trống!")]
        [DisplayName("Lý Do")]
        public string LyDoKyLuat { get; set; }
        [DisplayName("Số Tiền Kỷ Luật")]
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public float SoTienKyLuat { get; set; }

        [Required(ErrorMessage = "Ngày Kỷ Luật Không Được Bỏ Trống!")]
        [DataType(DataType.Date)]
        [DisplayName("Ngày Kỷ Luật")]
        public DateTime NgayKyLuat { get; set; }
        
        public ICollection<Luong>? Luong { get; set; }
    }
}
