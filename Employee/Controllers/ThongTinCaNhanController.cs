using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    public class ThongTinCaNhanController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ThongTinCaNhanController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            // Lấy tên người dùng từ HttpContext
            string userName = HttpContext.User.Identity.Name;

            // Lấy thông tin người dùng từ bảng NguoiDung dựa trên tên người dùng
            var user = await _context.NguoiDungs
                                     .FirstOrDefaultAsync(u => u.TenDangNhap == userName);

            if (user == null)
            {
                return NotFound(); // Trả về trang 404 nếu không tìm thấy người dùng
            }

            // Lấy thông tin nhân viên từ bảng NhanVien dựa trên NhanVienID từ bảng NguoiDung
            var currentUser = await _context.NhanViens
                                             .Include(n => n.ChucVu)
                                             .Include(n => n.ChuyenNganh)
                                             .Include(n => n.PhongBan)
                                             .Include(n => n.TrinhDo)
                                             .Where(n => n.ID == user.NhanVienID) // Dùng NhanVienID trong bảng NguoiDung
                                             .ToListAsync();

            if (currentUser == null || !currentUser.Any())
            {
                return NotFound(); // Trả về trang 404 nếu không tìm thấy thông tin của nhân viên
            }

            return View(currentUser); // Truyền thông tin của nhân viên vào view
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> CapNhat(int? id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["ChucVuID"] = new SelectList(_context.ChucVus, "ID", "TenChucVu", nhanVien.ChucVuID);
            ViewData["ChuyenMonID"] = new SelectList(_context.ChuyenNganhs, "ID", "TenChuyenNganh", nhanVien.ChuyenNganhID);
            ViewData["PhongBanId"] = new SelectList(_context.PhongBans, "ID", "TenPhongBan", nhanVien.PhongBanId);
            ViewData["TrinhDoID"] = new SelectList(_context.TrinhDos, "ID", "TenTrinhDo", nhanVien.TrinhDoID);
            return View(nhanVien);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,NhanVien nhanVien)
        {
            if (id != nhanVien.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ChucVuID"] = new SelectList(_context.ChucVus, "ID", "TenChucVu", nhanVien.ChucVuID);
            ViewData["ChuyenMonID"] = new SelectList(_context.ChuyenNganhs, "ID", "TenChuyenNganh", nhanVien.ChuyenNganhID);
            ViewData["PhongBanId"] = new SelectList(_context.PhongBans, "ID", "TenPhongBan", nhanVien.PhongBanId);
            ViewData["TrinhDoID"] = new SelectList(_context.TrinhDos, "ID", "TenTrinhDo", nhanVien.TrinhDoID);
            return View(nhanVien);
        }
        private bool NhanVienExists(int id)
        {
            return (_context.NhanViens?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
