using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class Luong
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Mã Nhân Viên Không Được Bỏ Trống!")]
        [DisplayName("Tên Nhân Viên")]
        public int NhanVienID { get; set; }
        [Required(ErrorMessage = "Tháng Không Được Bỏ Trống!")]
        [DisplayName("Tháng Nhận")]
        public int ThangNhanID { get; set; }


        [Required(ErrorMessage = "Lương Cơ Bản Không Được Bỏ Trống!")]
        [DisplayName("Lương Cơ Bản")]
        public int LuongCoBanID { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Tình Trạng Lương Không Được Bỏ Trống!")]
        [DisplayName("Tình Trạng Lương")]
        public string TinhTrangLuong { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        [Required(ErrorMessage = "Tổng Thu Nhập Không Được Bỏ Trống!")]
        [DisplayName("Tổng Thu Nhập")]
        public float TongThuNhap { get; set; }

        [Required(ErrorMessage = "Ngày Thanh Toán Không Được Bỏ Trống!")]
        [DisplayName("Ngày Thanh Toán")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime NgayThanhToan { get; set; }

 
        [DisplayName("Lý Do Khoản Trừ")]
        public string LyDoKyLuatID { get; set; }

        [DisplayName("Số Tiền Phạt")]
        public int KyLuatID { get; set; }

        [DisplayName("Lý Do Khen Thưởng")]
        public string LyDoKhenThuongID { get; set; }
        [DisplayName("Số Tiền Thưởng")]
        public int KhenThuongID { get; set; }


        [DisplayName("Khoản Trừ")]
        public KyLuat? KyLuat { get; set; }
        [DisplayName("Khen Thưởng")]
        public KhenThuong? KhenThuong { get; set; }

        [DisplayName("Nhân Viên")]
        public NhanVien? NhanVien { get; set; }
        public ThoiGianLamViec? ThoiGianLamViec { get; set; }

        internal object FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        internal float Sum(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
