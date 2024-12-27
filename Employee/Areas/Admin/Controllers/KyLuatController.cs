using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KyLuatController : Controller
    {
        private readonly DataContext _context;

        public KyLuatController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var kyLuat = await _context.KyLuats.ToListAsync();
            return View(kyLuat);
        }
        // GET: Admin/KyLuat/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/KyLuat/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi(KyLuat kyLuat)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaCoSo đã tồn tại
                var existingKyLuat = await _context.KyLuats
                    .FirstOrDefaultAsync(n => n.MaKyLuat == kyLuat.MaKyLuat);

                if (existingKyLuat != null)
                {
                    ModelState.AddModelError("MaKyLuat", "Mã kỷ luật đã tồn tại. Vui lòng nhập mã khác!");
                    return View(kyLuat);
                }
                // Lưu dữ liệu nếu mã nhân viên hợp lệ
                _context.Add(kyLuat);
                await _context.SaveChangesAsync();
                TempData["seccess"] = "Thêm kỷ luật thành công";
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
            return View(kyLuat);
        }
        // GET: KyLuat/Sua
        public async Task<IActionResult> Sua(int id)
        {
            // Lấy dữ liệu kỷ luật theo ID
            var kyLuat = await _context.KyLuats.FindAsync(id);

            // Nếu không tìm thấy kỷ luật, trả về 404
            if (kyLuat == null)
            {
                return NotFound();
            }

            return View(kyLuat);
        }

        // POST: KyLuat/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, KyLuat kyLuat)
        {
            if (id != kyLuat.ID)
            {
                return NotFound(); // Kiểm tra ID có khớp không
            }

            // Kiểm tra model hợp lệ
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu Mã Kỷ Luật đã tồn tại nhưng không phải của bản ghi hiện tại
                var existingKyLuat = await _context.KyLuats
                    .FirstOrDefaultAsync(n => n.MaKyLuat == kyLuat.MaKyLuat && n.ID != kyLuat.ID);

                if (existingKyLuat != null)
                {
                    // Thêm lỗi vào ModelState và hiển thị lại trang Sửa
                    ModelState.AddModelError("MaKyLuat", "Mã kỷ luật đã tồn tại. Vui lòng nhập mã khác!");
                    return View(kyLuat);
                }

                try
                {
                    // Cập nhật dữ liệu
                    _context.Update(kyLuat);
                    await _context.SaveChangesAsync();

                    // Thông báo thành công
                    TempData["success"] = "Cập nhật kỷ luật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KyLuatExists(kyLuat.ID))
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
            return View(kyLuat);
        }

        // Hàm kiểm tra tồn tại của kỷ luật
        private bool KyLuatExists(int id)
        {
            return _context.KyLuats.Any(e => e.ID == id);
        }

        //NhanVien/Xoa
        public async Task<IActionResult> Xoa(int id)
        {
            var kyLuat = await _context.KyLuats.FindAsync(id);
            _context.KyLuats.Remove(kyLuat);
            await _context.SaveChangesAsync();
            TempData["seccess"] = "Xoá kỷ luật thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
