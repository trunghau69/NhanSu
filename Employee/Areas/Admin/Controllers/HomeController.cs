using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }
        // GET: NhanVien
        public async Task<IActionResult> Index()
        {
            try
            {
                // Lấy tổng số lượng nhân viên từ cơ sở dữ liệu
                var totalEmployees = await _context.NhanViens.CountAsync();

                // Truyền tổng số lượng nhân viên vào ViewBag hoặc ViewData
                ViewBag.TotalEmployees = totalEmployees;

                // Lấy danh sách nhân viên và trả về view
                var nhanVien = _context.NhanViens.Include(n => n.ChucVu).Include(n => n.ChuyenNganh).Include(n => n.PhongBan).Include(n => n.TrinhDo);
                return View(await nhanVien.ToListAsync());
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return BadRequest($"Error: {ex.Message}");
            }
        }
        public async Task<IActionResult> TongNV()
        {
            try
            {
                // Lấy tổng số lượng nhân viên từ cơ sở dữ liệu
                var totalEmployees = await _context.NhanViens.CountAsync();

                // Truyền tổng số lượng nhân viên vào ViewBag hoặc ViewData
                ViewBag.TotalEmployees = totalEmployees;

                // Trả về view để hiển thị tổng số lượng nhân viên
                return View();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
