using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee.Helpers; // Đảm bảo đúng namespace

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NghiPhepController : Controller
    {
        private readonly DataContext _context;

        public NghiPhepController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var nghiPhep = _context.NghiPheps.Include(np => np.NhanVien);
            return View(await nghiPhep.ToListAsync());
        }
        // GET: Admin/NghiPhep/ThemMoi
        public IActionResult ThemMoi()
        {



        //    // Tạo SelectList cho trạng thái nghỉ phép từ enum
        //    ViewBag.TrangThaiNghiPhep = new SelectList(
        //    Enum.GetValues(typeof(TrangThaiNghiPhep))
        //        .Cast<TrangThaiNghiPhep>()
        //        .Select(e => new SelectListItem
        //        {
        //            Value = e.ToString(),
        //            Text = e.GetDisplayName() // Sử dụng phương thức mở rộng để lấy DisplayName từ attribute
        //        }),
        //    "Value",
        //    "Text"
        //);


            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien");
            return View();
        }
        // POST: Admin/NghiPhep/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi(NghiPhep nghiPhep)
        {
         //   // Tạo SelectList cho trạng thái nghỉ phép từ enum
         //   ViewBag.TrangThaiNghiPhep = new SelectList(
         //    Enum.GetValues(typeof(TrangThaiNghiPhep))
         //        .Cast<TrangThaiNghiPhep>()
         //        .Select(e => new SelectListItem
         //        {
         //            Value = e.ToString(),
         //            Text = e.GetDisplayName() // Sử dụng phương thức mở rộng để lấy DisplayName từ attribute
         //        }),
         //    "Value",
         //    "Text"
         //);



            // Tạo SelectList cho nhân viên
            

            // Kiểm tra tính hợp lệ của ModelState
            if (ModelState.IsValid)
            {
                

                // Kiểm tra xem có trùng lặp thời gian nghỉ phép cho cùng một nhân viên không
                var existingNghiPhep = await _context.NghiPheps
                    .FirstOrDefaultAsync(np => np.NhanVienID == nghiPhep.NhanVienID
                                            && np.TrangThai == TrangThaiNghiPhep.DaDuyet
                                            && ((nghiPhep.NgayBatDau >= np.NgayBatDau && nghiPhep.NgayBatDau <= np.NgayKetThuc) ||
                                                (nghiPhep.NgayKetThuc >= np.NgayBatDau && nghiPhep.NgayKetThuc <= np.NgayKetThuc)));

                if (existingNghiPhep != null)
                {
                    // Thêm lỗi vào ModelState nếu nghỉ phép trùng lặp
                    ModelState.AddModelError("", "Nhân viên đã có nghỉ phép trùng với thời gian này.");
                    return View(nghiPhep);
                }

                // Lưu thông tin nghỉ phép vào cơ sở dữ liệu
                _context.NghiPheps.Add(nghiPhep);
                await _context.SaveChangesAsync();

                // Thông báo thành công
                TempData["success"] = "Thêm mới nghỉ phép thành công!";
                return RedirectToAction(nameof(Index)); // Chuyển hướng về danh sách
            }

            // Nếu ModelState không hợp lệ, quay lại form thêm mới với lỗi
            TempData["error"] = "Dữ liệu nhập không hợp lệ!";
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", nghiPhep.NhanVienID);
            ViewData["TrangThai"] = new SelectList(_context.NghiPheps, "ID", "TrangThai", nghiPhep.TrangThai);
            return View(nghiPhep);
        }
        public int TinhSoNgayNghi(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            // Nếu ngày bắt đầu và ngày kết thúc trùng nhau, tính 1 ngày nghỉ
            if (ngayBatDau == ngayKetThuc)
            {
                return ngayBatDau.DayOfWeek == DayOfWeek.Saturday || ngayBatDau.DayOfWeek == DayOfWeek.Sunday ? 0 : 1;
            }

            // Tính tổng số ngày trong khoảng thời gian
            TimeSpan khoangThoiGian = ngayKetThuc - ngayBatDau;
            int soNgayNghi = khoangThoiGian.Days + 1; // Cộng thêm 1 để bao gồm cả ngày kết thúc

            // Loại bỏ các ngày cuối tuần (Thứ Bảy và Chủ Nhật)
            for (DateTime date = ngayBatDau; date <= ngayKetThuc; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    soNgayNghi--; // Giảm số ngày nghỉ nếu là cuối tuần
                }
            }

            return soNgayNghi;
        }

    }
}
