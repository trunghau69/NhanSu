using Employee.Models.Repository;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhongBanController : Controller
    {
        private readonly DataContext _context;

        public PhongBanController(DataContext context)
        {
            _context = context;
        }

        // Danh sách phòng ban
        public async Task<IActionResult> Index()
        {
            var phongBans = await _context.PhongBans.ToListAsync();
            return View(phongBans);
        }
        // GET: Admin/PhongBan/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/PhongBan/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi([Bind("ID,MaPhongBan,TenPhongBan")] PhongBan phongBan)
        {
            if (!IsDuplicateCode(phongBan.MaPhongBan))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(phongBan);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(phongBan);
                }
            }
            else
            {
                ModelState.AddModelError("MaPhongBan", "Mã Đã Tồn Tại!");
                return View(phongBan);
            }
        }
        public bool IsDuplicateCode(string code)
        {
            return _context.PhongBans.Any(b => b.MaPhongBan == code);
        }

        // GET: Admin/PhongBan/Sua
        public async Task<IActionResult> Sua(int? id)
        {
            var phongBan = await _context.PhongBans.FindAsync(id);
            return View(phongBan);
        }

        // POST: Admin/PhongBan/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, [Bind("ID,MaPhongBan,TenPhongBan")] PhongBan phongBan)
        {
            if (id != phongBan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PhongBans.Any(e => e.ID == id))
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
            return View(phongBan);
        }

        private bool PhongBanExists(int id)
        {
            return (_context.PhongBans?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Xoa(int id)
        {
            var phongBan = await _context.PhongBans.FindAsync(id);
            _context.PhongBans.Remove(phongBan);
            await _context.SaveChangesAsync();
            TempData["success"] = "Phòng ban đã xoá";
            return RedirectToAction("Index");
        }
    }
}
