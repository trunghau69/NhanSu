using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LuongController : Controller
    {
        private readonly DataContext _context;

        public LuongController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Luong
        public async Task<IActionResult> Index()
        {
            var luong = _context.Luongs.Include(l => l.KhenThuong).Include(l => l.KyLuat).Include(l => l.NhanVien).Include(l => l.ThoiGianLamViec);
            return View(await luong.ToListAsync());
        }
        // GET: Admin/Luong/ThemMoi
        public IActionResult ThemMoi()
        {
            ViewData["ThangID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "Thang");
            ViewData["LyDoKhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "LyDoKhenThuong");
            ViewData["KhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "TienThuong");
            ViewData["LyDoKyLuatID"] = new SelectList(_context.KyLuats, "ID", "LyDoKyLuat");
            ViewData["KyLuatID"] = new SelectList(_context.KyLuats, "ID", "SoTienKyLuat");
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien");
            ViewData["ThoiGianLamViecID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "SoNgayCong");
            return View();
        }
        // POST: Admin/Luong/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi(Luong luong)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Thêm mới dữ liệu lương vào cơ sở dữ liệu
                    _context.Luongs.Add(luong);
                    await _context.SaveChangesAsync();

                    // Thông báo thêm mới thành công
                    TempData["success"] = "Thêm mới thông tin lương thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi nếu xảy ra ngoại lệ
                    TempData["error"] = "Đã xảy ra lỗi khi thêm mới lương: " + ex.Message;
                }
            }

            // Nếu ModelState không hợp lệ hoặc có lỗi xảy ra, hiển thị lại form với dữ liệu hiện tại
            ViewData["ThangID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "Thang", luong.ThangNhanID);
            ViewData["LyDoKhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "LyDoKhenThuong", luong.LyDoKhenThuongID);
            ViewData["KhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "TienThuong", luong.KhenThuongID);
            ViewData["LyDoKhoanTruID"] = new SelectList(_context.KyLuats, "ID", "LyDoKhoanTru", luong.LyDoKyLuatID);
            ViewData["KhoanTruID"] = new SelectList(_context.KyLuats, "ID", "SoTienKhoanTru", luong.KyLuatID);
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", luong.NhanVienID);
            ViewData["ThoiGianLamViecID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "SoNgayCong", luong.ThoiGianLamViec);
            return View(luong);
        }
        public IActionResult GetThang(int NhanVienID)
        {
            var thoiGianLamViecList = _context.ThoiGianLamViecs.Where(t => t.NhanVienID == NhanVienID).ToList();

            if (thoiGianLamViecList != null && thoiGianLamViecList.Any())
            {
                var thangList = thoiGianLamViecList.Select(t => new { id = t.ID, thang = t.Thang }).ToList();
                return Json(thangList); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về số ngày công
            }
        }
        public IActionResult GetSoNgayCong(int NhanVienID, int Thang)
        {
            var thoiGianLamViec = _context.ThoiGianLamViecs.FirstOrDefault(t => t.NhanVienID == NhanVienID && t.Thang == Thang);
            if (thoiGianLamViec != null)
            {
                var soNgayCong = new { id = thoiGianLamViec.ID, soNgayCong = thoiGianLamViec.SoNgayCong };
                return Json(soNgayCong); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về số ngày công cho tháng đó của nhân viên
            }
        }
        public IActionResult GetKhenThuong(int KhenThuongID)
        {
            var lyDoKhenThuong = _context.KhenThuongs.FirstOrDefault(t => t.ID == KhenThuongID);
            if (lyDoKhenThuong != null)
            {
                var khenThuong = new { id = lyDoKhenThuong.ID, khenThuong = lyDoKhenThuong.TienThuong };
                return Json(khenThuong); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về số ngày công
            }
        }
        public IActionResult GetKyLuat(int KyLuatID)
        {
            var lyDoKyLuat = _context.KyLuats.FirstOrDefault(t => t.ID == KyLuatID);
            if (lyDoKyLuat != null)
            {
                var kyLuat = new { id = lyDoKyLuat.ID, kyLuat = lyDoKyLuat.SoTienKyLuat };
                return Json(kyLuat); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về số ngày công
            }
        }
        // GET: Admin/Luong/Sua
        public async Task<IActionResult> Sua(int? id)
        {
            if (id == null || _context.Luongs == null)
            {
                return NotFound();
            }

            var luong = await _context.Luongs.FindAsync(id);
            if (luong == null)
            {
                return NotFound();
            }

            ViewData["ThangID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "Thang", luong.ThoiGianLamViec);

            ViewData["LyDoKhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "LyDoKhenThuong", luong.KhenThuongID);

            ViewData["KhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "TienThuong", luong.KhenThuongID);
            ViewData["LyDoKhoanTruID"] = new SelectList(_context.KyLuats, "ID", "LyDoKhoanTru", luong.KyLuatID);
            ViewData["KhoanTruID"] = new SelectList(_context.KyLuats, "ID", "SoTienKhoanTru", luong.KyLuatID);
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", luong.NhanVienID);
            ViewData["ThoiGianLamViecID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "SoNgayCong", luong.ThoiGianLamViec);
            return View(luong);
        }
        // POST: Admin/Luong/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, Luong luong)
        {
            if (id != luong.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật dữ liệu lương vào cơ sở dữ liệu
                    _context.Update(luong);
                    await _context.SaveChangesAsync();

                    // Thông báo cập nhật thành công
                    TempData["success"] = "Cập nhật thông tin lương thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LuongExists(luong.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Thông báo lỗi nếu xảy ra lỗi cạnh tranh cập nhật
                        TempData["error"] = "Đã xảy ra lỗi cạnh tranh dữ liệu khi cập nhật lương!";
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi nếu xảy ra ngoại lệ
                    TempData["error"] = "Đã xảy ra lỗi khi cập nhật thông tin lương: " + ex.Message;
                }
            }

            // Nếu dữ liệu không hợp lệ hoặc có lỗi, trả về view với dữ liệu hiện tại
            ViewData["ThangID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "Thang", luong.ThangNhanID);
            ViewData["LyDoKhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "LyDoKhenThuong", luong.LyDoKhenThuongID);
            ViewData["KhenThuongID"] = new SelectList(_context.KhenThuongs, "ID", "TienThuong", luong.KhenThuongID);
            ViewData["LyDoKhoanTruID"] = new SelectList(_context.KyLuats, "ID", "LyDoKhoanTru", luong.LyDoKyLuatID);
            ViewData["KhoanTruID"] = new SelectList(_context.KyLuats, "ID", "SoTienKhoanTru", luong.KyLuatID);
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", luong.NhanVienID);
            ViewData["ThoiGianLamViecID"] = new SelectList(_context.ThoiGianLamViecs, "ID", "SoNgayCong", luong.ThoiGianLamViec);
            return View(luong);
        }
        private bool LuongExists(int id)
        {
            return _context.Luongs.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Xoa(int id)
        {
            var luong = await _context.Luongs.FindAsync(id);
            _context.Luongs.Remove(luong);
            await _context.SaveChangesAsync();
            TempData["success"] = "Lương đã xoá";
            return RedirectToAction("Index");
        }
    }
}
