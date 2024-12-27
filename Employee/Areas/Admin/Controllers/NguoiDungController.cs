using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NguoiDungController : Controller
    {
        private readonly DataContext _context;

        public NguoiDungController(DataContext context)
        {
            _context = context;
        }

        // GET: NguoiDung
        public async Task<IActionResult> Index()
        {
            var nguoiDung = _context.NguoiDungs.Include(t => t.NhanVien);
            return View(await nguoiDung.ToListAsync());
        }
        // GET: NguoiDung/ThemMoi
        public IActionResult ThemMoi()
        {
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien");
            ViewData["EmailID"] = new SelectList(_context.NhanViens, "ID", "Email");
            return View();
        }
        // POST: NguoiDung/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng lặp Email hoặc Tên đăng nhập
                var existingUser = await _context.NguoiDungs
                    .FirstOrDefaultAsync(u => u.EmailID == nguoiDung.EmailID || u.TenDangNhap == nguoiDung.TenDangNhap);

                if (existingUser != null)
                {
                    if (existingUser.EmailID == nguoiDung.EmailID)
                    {
                        ModelState.AddModelError("EmailID", "Email đã tồn tại. Vui lòng nhập email khác!");
                    }

                    if (existingUser.TenDangNhap == nguoiDung.TenDangNhap)
                    {
                        ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác!");
                    }

                    return View(nguoiDung);
                }

                // Hash mật khẩu
                nguoiDung.MatKhau = BCrypt.Net.BCrypt.HashPassword(nguoiDung.MatKhau);

                // Lưu vào cơ sở dữ liệu
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm mới người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Thêm mới người dùng thất bại. Vui lòng kiểm tra lại dữ liệu!";
            return View(nguoiDung);
        }
        public IActionResult GetEmail(int NhanVienID)
        {
            var email = _context.NhanViens.FirstOrDefault(t => t.ID == NhanVienID);
            if (email != null)
            {
                var emaill = new { id = email.Email, email = email.Email };
                return Json(emaill); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về số ngày công
            }
        }
        // GET: NguoiDung/Sua
        public async Task<IActionResult> Sua(int? id)
        {
            if (id == null || _context.NguoiDungs == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            ViewData["NhanVienID"] = new SelectList(_context.NhanViens, "ID", "TenNhanVien", nguoiDung.NhanVienID);
            ViewData["EmailID"] = new SelectList(_context.NhanViens, "ID", "Email", nguoiDung.EmailID);
            return View(nguoiDung);
        }
        // POST: NguoiDung/ChinhSua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, NguoiDung nguoiDung)
        {
            if (id != nguoiDung.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra trùng lặp Email hoặc Tên đăng nhập, ngoại trừ người dùng hiện tại
                    var existingUser = await _context.NguoiDungs
                        .FirstOrDefaultAsync(u => (u.EmailID == nguoiDung.EmailID || u.TenDangNhap == nguoiDung.TenDangNhap) && u.ID != id);

                    if (existingUser != null)
                    {
                        if (existingUser.EmailID == nguoiDung.EmailID)
                        {
                            ModelState.AddModelError("EmailID", "Email đã tồn tại. Vui lòng nhập email khác!");
                        }

                        if (existingUser.TenDangNhap == nguoiDung.TenDangNhap)
                        {
                            ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác!");
                        }

                        return View(nguoiDung);
                    }

                    // Tìm người dùng hiện tại trong cơ sở dữ liệu
                    var userToUpdate = await _context.NguoiDungs.FindAsync(id);
                    if (userToUpdate == null)
                    {
                        return NotFound();
                    }

                    // Nếu không thay đổi mật khẩu, giữ nguyên mật khẩu cũ
                    if (!string.IsNullOrEmpty(nguoiDung.MatKhau))
                    {
                        // Hash mật khẩu mới
                        nguoiDung.MatKhau = BCrypt.Net.BCrypt.HashPassword(nguoiDung.MatKhau);
                    }
                    else
                    {
                        nguoiDung.MatKhau = userToUpdate.MatKhau; // Giữ mật khẩu cũ nếu không nhập mới
                    }

                    // Cập nhật các thông tin khác
                    userToUpdate.EmailID = nguoiDung.EmailID;
                    userToUpdate.TenDangNhap = nguoiDung.TenDangNhap;
                    userToUpdate.MatKhau = nguoiDung.MatKhau;
                    userToUpdate.Quyen = nguoiDung.Quyen;
                    userToUpdate.NhanVienID = nguoiDung.NhanVienID;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();

                    TempData["success"] = "Chỉnh sửa người dùng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            TempData["error"] = "Chỉnh sửa người dùng thất bại. Vui lòng kiểm tra lại dữ liệu!";
            return View(nguoiDung);
        }
        private bool NguoiDungExists(int id)
        {
            return _context.NguoiDungs.Any(e => e.ID == id);
        }
        //NguoiDung/Xoa
        public async Task<IActionResult> Xoa(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            _context.NguoiDungs.Remove(nguoiDung);
            await _context.SaveChangesAsync();
            TempData["seccess"] = "Xoá người dùng thành công";
            return RedirectToAction(nameof(Index));
        }

    }
}
