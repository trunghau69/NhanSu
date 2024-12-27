using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuyenNganhController : Controller
    {
        private readonly DataContext _context;

        public ChuyenNganhController(DataContext context)
        {
            _context = context;
        }

        // Danh sách chuyên ngành
        public async Task<IActionResult> Index()
        {
            var chuyenNganhs = await _context.ChuyenNganhs.ToListAsync();
            return View(chuyenNganhs);
        }
        // GET: Admin/ChuyenNganh/ThemMoi
        public IActionResult ThemMoi()
        {
            return View();
        }
        // POST: Admin/ChucVu/ThemMoi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi([Bind("ID,MaChuyenNganh,TenChuyenNganh")] ChuyenNganh chuyenNganh)
        {
            if (!IsDuplicateCode(chuyenNganh.MaChuyenNganh))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(chuyenNganh);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(chuyenNganh);
                }
            }
            else
            {
                ModelState.AddModelError("MaChuyenNganh", "Mã Đã Tồn Tại!");
                return View(chuyenNganh);
            }
        }
        public bool IsDuplicateCode(string code)
        {
            return _context.ChuyenNganhs.Any(b => b.MaChuyenNganh == code);
        }

        // GET: Admin/ChuyenNganh/Sua
        public async Task<IActionResult> Sua(int? id)
        {
            var chuyenNganh = await _context.ChuyenNganhs.FindAsync(id);
            return View(chuyenNganh);
        }

        // POST: Admin/ChuyenNganh/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, [Bind("ID,MaChuyenNganh,TenChuyenNganh")] ChuyenNganh chuyenNganh)
        {
            if (id != chuyenNganh.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuyenNganh);
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
            return View(chuyenNganh);
        }

        private bool ChuyenNganhExists(int id)
        {
            return (_context.ChuyenNganhs?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Xoa(int id)
        {
            var chuyenNganh = await _context.ChuyenNganhs.FindAsync(id);
            _context.ChuyenNganhs.Remove(chuyenNganh);
            await _context.SaveChangesAsync();
            TempData["success"] = "Chuyên ngành đã xoá";
            return RedirectToAction("Index");
        }
    }
}
