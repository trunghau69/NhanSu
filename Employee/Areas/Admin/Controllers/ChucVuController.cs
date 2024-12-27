using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChucVuController : Controller
    {
        private readonly DataContext _context;

        public ChucVuController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.ChucVus != null ?
                        View(await _context.ChucVus.ToListAsync()) :
                        Problem("Entity set 'DataContext.ChucVus'  is null.");
        }
        // GET: Admin/ChucVu/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/ChucVu/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi([Bind("ID,MaChucVu,TenChucVu,MoTa")] ChucVu chucVu)
        {
            if (!IsDuplicateCode(chucVu.MaChucVu))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(chucVu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(chucVu);
                }
            }
            else
            {
                ModelState.AddModelError("MaChucVu", "Mã Đã Tồn Tại!");
                return View(chucVu);
            }
        }
        public bool IsDuplicateCode(string code)
        {
            return _context.ChucVus.Any(b => b.MaChucVu == code);
        }

        // GET: Admin/ChucVu/Edit/5
        public async Task<IActionResult> Sua(int? id)
        {
            

            var chucVu = await _context.ChucVus.FindAsync(id);
            return View(chucVu);
        }

        // POST: Admin/ChucVu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, [Bind("ID,MaChucVu,TenChucVu,MoTa")] ChucVu chucVu)
        {
            if (id != chucVu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ChucVus.Any(e => e.ID == id))
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
            return View(chucVu);
        }

        private bool ChucVuExists(int id)
        {
            return (_context.ChucVus?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Xoa(int id)
        {
            ChucVu chucVu = await _context.ChucVus.FindAsync(id);
            _context.ChucVus.Remove(chucVu);
            await _context.SaveChangesAsync();
            TempData["success"] = "Chức vụ đã xoá";
            return RedirectToAction("Index");
        }

    }
}
