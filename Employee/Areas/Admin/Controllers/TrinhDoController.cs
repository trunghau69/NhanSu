using Employee.Models.Repository;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrinhDoController : Controller
    {
        private readonly DataContext _context;

        public TrinhDoController(DataContext context)
        {
            _context = context;
        }

        // Danh sách trình độ học vấn
        public async Task<IActionResult> Index()
        {
            var trinhDos = await _context.TrinhDos.ToListAsync();
            return View(trinhDos);
        }
        // GET: Admin/TrinhDo/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/TrinhDo/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi([Bind("ID,MaTrinhDo,TenTrinhDo")] TrinhDo trinhDo)
        {
            if (!IsDuplicateCode(trinhDo.MaTrinhDo))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(trinhDo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(trinhDo);
                }
            }
            else
            {
                ModelState.AddModelError("MaTrinhDo", "Mã Đã Tồn Tại!");
                return View(trinhDo);
            }
        }
        public bool IsDuplicateCode(string code)
        {
            return _context.TrinhDos.Any(b => b.MaTrinhDo == code);
        }

        // GET: Admin/TrinhDo/Sua
        public async Task<IActionResult> Sua(int? id)
        {
            var trinhDo = await _context.TrinhDos.FindAsync(id);
            return View(trinhDo);
        }

        // POST: Admin/TrinhDo/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, [Bind("ID,MaTrinhDo,TenTrinhDo")] TrinhDo trinhDo)
        {
            if (id != trinhDo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trinhDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TrinhDos.Any(e => e.ID == id))
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
            return View(trinhDo);
        }

        private bool TrinhDoExists(int id)
        {
            return (_context.TrinhDos?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Xoa(int id)
        {
            var trinhDo = await _context.TrinhDos.FindAsync(id);
            _context.TrinhDos.Remove(trinhDo);
            await _context.SaveChangesAsync();
            TempData["success"] = "Trình độ đã xoá";
            return RedirectToAction("Index");
        }
    }
}
