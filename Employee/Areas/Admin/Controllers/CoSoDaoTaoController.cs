using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoSoDaoTaoController : Controller
    {
        private readonly DataContext _context;

        public CoSoDaoTaoController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/CoSoDaoTao
        public IActionResult Index()
        {
            var coSoDaoTaos = _context.CoSoDaoTaos.ToList();
            return View(coSoDaoTaos);
        }

        // GET: Admin/CoSoDaoTao/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/CoSoDaoTao/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi(CoSoDaoTao coSo)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaCoSo đã tồn tại
                var existingCoSo = await _context.CoSoDaoTaos
                    .FirstOrDefaultAsync(n => n.MaCoSo == coSo.MaCoSo);

                if (existingCoSo != null)
                {
                    ModelState.AddModelError("MaCoSo", "Mã cơ sở đã tồn tại. Vui lòng nhập mã khác!");
                    return View(coSo);
                }
                // Lưu dữ liệu nếu mã nhân viên hợp lệ
                _context.Add(coSo);
                await _context.SaveChangesAsync();
                TempData["seccess"] = "Thêm cơ sở thành công";
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
            return View(coSo);
        }
        //CoSoDaoTao/Xoa
        public async Task<IActionResult> Xoa(int id)
        {
            var coSo = await _context.CoSoDaoTaos.FindAsync(id);
            _context.CoSoDaoTaos.Remove(coSo);
            await _context.SaveChangesAsync();
            TempData["seccess"] = "Xoá cơ sở thành công";
            return RedirectToAction(nameof(Index));
        }

        //GET: CoSoDaoTao/Sua
        public async Task<IActionResult> Sua(int id)
        {
            var coSo = await _context.CoSoDaoTaos.FindAsync(id);
            return View(coSo);
        }
        // POST: Admin/CoSoDaoTao/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(CoSoDaoTao coSo)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaCoSo đã tồn tại
                var existingCoSo = await _context.CoSoDaoTaos
                    .FirstOrDefaultAsync(n => n.MaCoSo == coSo.MaCoSo);

                if (existingCoSo != null)
                {
                    ModelState.AddModelError("MaCoSo", "Mã cơ sở đã tồn tại. Vui lòng nhập mã khác!");
                    return View(coSo);
                }
                // Lưu dữ liệu nếu mã nhân viên hợp lệ
                _context.Update(coSo);
                await _context.SaveChangesAsync();
                TempData["seccess"] = "Cập nhật cơ sở thành công";
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
            return View(coSo);
        }
    }
}
