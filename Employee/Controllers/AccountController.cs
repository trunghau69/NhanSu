using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Employee.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Employee.Models;
using BC = BCrypt.Net.BCrypt;
using static Employee.Models.NguoiDung;
namespace Employee.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string? successMessage)
        {
            // Lấy tên người đăng nhập từ HttpContext.User
            var userName = User.Identity.Name;

            // Tìm kiếm thông tin của người dùng đăng nhập trong cơ sở dữ liệu
            var nhanVien = await _context.NhanViens
                .Include(n => n.ChucVu)
                .Include(n => n.ChuyenNganh)
                .Include(n => n.PhongBan)
                .Include(n => n.TrinhDo)
                .FirstOrDefaultAsync(n => n.TenNhanVien == userName);

            // Nếu không tìm thấy thông tin người dùng, trả về 404
            if (nhanVien == null)
            {
                return NotFound();
            }

            // Nếu tìm thấy, trả về view với thông tin người dùng
            return View(nhanVien);
        }
        public IActionResult Login(string? ReturnUrl)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                // Kiểm tra vai trò của người dùng
                if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
                {
                    // Nếu là admin, chuyển đến trang admin
                    return LocalRedirect(ReturnUrl ?? "/Admin/Home/Index");
                } 
                else
                {
                    // Nếu không phải admin, chuyển đến trang người dùng
                    return LocalRedirect(ReturnUrl ?? "/Home/Index");
                }
            }
            else
            {
                // Nếu chưa đăng nhập thì chuyển đến trang đăng nhập
                ViewBag.LienKetChuyenTrang = ReturnUrl ?? "/Account/Login";
                return View();
            }
        }
        // Xử lý logic đăng nhập
        [HttpPost]
        public async Task<IActionResult> Login(DangNhap dangNhap)
        {
            if (ModelState.IsValid)
            {
                var nguoiDung = _context.NguoiDungs.Where(r => r.TenDangNhap == dangNhap.TenDangNhap).SingleOrDefault();

                if (nguoiDung == null || !BC.Verify(dangNhap.MatKhau, nguoiDung.MatKhau))
                {
                    TempData["ThongBaoLoi"] = "Tài khoản không tồn tại trong hệ thống.";
                    return View(dangNhap);
                }
                else
                {
                    string role = "";
                    if (nguoiDung.Quyen == NguoiDung.QuyenHan.Admin)
                    {
                        role = "Admin";
                    }
                    else
                    {
                        role = "NhanVien";
                    }
                    //var quyenClaimValue = nguoiDung.Quyen.ToString();

                    var claims = new List<Claim>
                    {
                        new Claim("ID", nguoiDung.ID.ToString()),
                        new Claim(ClaimTypes.Name, nguoiDung.TenDangNhap),
                        new Claim("TenDangNhap", nguoiDung.TenDangNhap),
                        new Claim(ClaimTypes.Role, role)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = dangNhap.DuyTriDangNhap
                    };
                    // Đăng nhập hệ thống
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                  new ClaimsPrincipal(claimsIdentity),
                                                  authProperties);
                    return LocalRedirect(dangNhap.LienKetChuyenTrang ??
                        (nguoiDung.Quyen == NguoiDung.QuyenHan.Admin ? "/Admin" : "/NhanVien"));

                }
            }
            return View(dangNhap);
        }



        // Xử lý logic đăng xuất
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
