using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhenThuongController : Controller
    {
        private readonly DataContext _context;

        public KhenThuongController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var khenThuong = await _context.KhenThuongs.ToListAsync();
            return View(khenThuong);
        }
        // GET: Admin/KhenThuong/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/KhenThuong/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi(KhenThuong khenThuong)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaKhenThuong đã tồn tại
                var existingKhenThuong = await _context.KhenThuongs
                    .FirstOrDefaultAsync(n => n.MaKhenThuong == khenThuong.MaKhenThuong);

                if (existingKhenThuong != null)
                {
                    ModelState.AddModelError("MaKhenThuong", "Mã khen thưởng đã tồn tại. Vui lòng nhập mã khác!");
                    return View(khenThuong);
                }
                // Lưu dữ liệu nếu mã khen thưởng hợp lệ
                _context.Add(khenThuong);
                await _context.SaveChangesAsync();
                TempData["seccess"] = "Thêm khen thưởng thành công";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Model có một vài thứ bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(khenThuong);
        }
        // GET: Admin/KhenThuong/Sua
        public async Task<IActionResult> Sua(int id)
        {
            // Lấy dữ liệu khen thưởng theo ID
            var khenThuong = await _context.KhenThuongs.FindAsync(id);

            // Nếu không tìm thấy khen thưởng, trả về 404
            if (khenThuong == null)
            {
                return NotFound();
            }

            return View(khenThuong);
        }

        // POST: Admin/KhenThuong/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, KhenThuong khenThuong)
        {
            if (id != khenThuong.ID)
            {
                return NotFound(); // Kiểm tra ID có khớp không
            }

            // Kiểm tra model hợp lệ
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu Mã Khen Thưởng đã tồn tại nhưng không phải của bản ghi hiện tại
                var existingKhenThuong = await _context.KhenThuongs
                    .FirstOrDefaultAsync(n => n.MaKhenThuong == khenThuong.MaKhenThuong && n.ID != khenThuong.ID);

                if (existingKhenThuong != null)
                {
                    // Thêm lỗi vào ModelState và hiển thị lại trang Sửa
                    ModelState.AddModelError("MaKhenThuong", "Mã khen thưởng đã tồn tại. Vui lòng nhập mã khác!");
                    return View(khenThuong);
                }

                try
                {
                    // Cập nhật dữ liệu
                    _context.Update(khenThuong);
                    await _context.SaveChangesAsync();

                    // Thông báo thành công
                    TempData["success"] = "Cập nhật khen thưởng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhenThuongExists(khenThuong.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Nếu lỗi khác, ném ngoại lệ
                    }
                }
            }

            // Nếu ModelState không hợp lệ, hiển thị lại trang Sửa với lỗi
            return View(khenThuong);
        }

        // Hàm kiểm tra tồn tại của khen thưởng
        private bool KhenThuongExists(int id)
        {
            return _context.KhenThuongs.Any(e => e.ID == id);
        }


        //NhanVien/Xoa
        public async Task<IActionResult> Xoa(int id)
        {
            var khenThuong = await _context.KhenThuongs.FindAsync(id);
            _context.KhenThuongs.Remove(khenThuong);
            await _context.SaveChangesAsync();
            TempData["seccess"] = "Xoá khen thưởng thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
