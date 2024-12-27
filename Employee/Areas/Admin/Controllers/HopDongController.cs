using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HopDongController : Controller
    {
        private readonly DataContext _context;

        public HopDongController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var hopDong = _context.HopDongs.Include(hd => hd.NhanVien);
            return View(await hopDong.ToListAsync());
        }
        public IActionResult ThemMoi()
        {
            var hopDong = new HopDong();
            hopDong.NgayBatDau = DateTime.Today;
            hopDong.NgayKetThuc = DateTime.Today;
            hopDong.NgayKy = DateTime.Today;
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien");
            return View(hopDong);
        }
        // POST: Admin/HopDong/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi (HopDong hopDong)
        {
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien",hopDong.NhanVienID);
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaHopDong đã tồn tại
                var existingHopDong = await _context.HopDongs
                    .FirstOrDefaultAsync(n => n.MaHopDong == hopDong.MaHopDong);

                if (existingHopDong != null)
                {
                    ModelState.AddModelError("MaHopDong", "Mã hợp đồng đã tồn tại. Vui lòng nhập mã khác!");
                    return View(hopDong);
                }
                // Lưu dữ liệu nếu mã hợp đồng hợp lệ
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                TempData["seccess"] = "Thêm hợp đồng thành công";
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
            return View(hopDong);
        }

        // GET: HopDong/Sua
        public async Task<IActionResult> Sua(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy hợp đồng
            }

            hopDong.NgayBatDau = DateTime.Today;
            hopDong.NgayKetThuc = DateTime.Today;
            hopDong.NgayKy = DateTime.Today;

            // Lấy danh sách nhân viên để hiển thị chọn
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", hopDong.NhanVienID);
            return View(hopDong);
        }

        // POST: HopDong/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(HopDong hopDong)
        {
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", hopDong.NhanVienID);

            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaHopDong đã tồn tại nhưng không phải bản ghi hiện tại
                var existingHopDong = await _context.HopDongs
                    .FirstOrDefaultAsync(n => n.MaHopDong == hopDong.MaHopDong && n.ID != hopDong.ID);

                if (existingHopDong != null)
                {
                    ModelState.AddModelError("MaHopDong", "Mã hợp đồng đã tồn tại. Vui lòng nhập mã khác!");
                    return View(hopDong);
                }

                try
                {
                    // Cập nhật hợp đồng
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();

                    TempData["success"] = "Cập nhật hợp đồng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.ID))
                    {
                        return NotFound(); // Nếu hợp đồng không tồn tại trong DB, trả về lỗi
                    }
                    else
                    {
                        throw; // Nếu có lỗi khác, ném ngoại lệ
                    }
                }
            }
            else
            {
                TempData["error"] = "Dữ liệu không hợp lệ!";
                return View(hopDong); // Trả về view với lỗi ModelState
            }
        }

        // Hàm kiểm tra sự tồn tại của hợp đồng
        private bool HopDongExists(int id)
        {
            return _context.HopDongs.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Xoa(int id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            _context.HopDongs.Remove(hopDong);
            await _context.SaveChangesAsync();
            TempData["success"] = "Hợp đồng đã xoá";
            return RedirectToAction("Index");
        }
    }
}
