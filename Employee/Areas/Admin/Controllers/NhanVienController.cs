using Employee.Models;
using Employee.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Slugify;

namespace Employee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public NhanVienController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var nhanViens = _context.NhanViens.Include(n => n.ChucVu).Include(n => n.ChuyenNganh).Include(n => n.PhongBan).Include(n => n.TrinhDo);
            return View(await nhanViens.ToListAsync());
        }

        // GET: NhanVien/ThemMoi
        public IActionResult ThemMoi()
        {
            // Tạo model mới cho việc thêm nhân viên
            var nhanVien = new NhanVien();


            nhanVien.NgaySinh = DateTime.Today;
            ViewData["ChucVuID"] = new SelectList(_context.ChucVus, "ID", "TenChucVu");
            ViewData["ChuyenNganhID"] = new SelectList(_context.ChuyenNganhs, "ID", "TenChuyenNganh");
            ViewData["PhongBanID"] = new SelectList(_context.PhongBans, "ID", "TenPhongBan");
            ViewData["TrinhDoID"] = new SelectList(_context.TrinhDos, "ID", "TenTrinhDo");
            return View(nhanVien);
        }
        public IActionResult GetEmail(int NhanVienID)
        {
            // Tìm số ngày công của nhân viên dựa trên ID nhân viên và trả về dữ liệu (ví dụ: số ngày công)
            //var dMTapThe = _context.DMTapThe.Where(t => t.ID == mau11id).FirstOrDefault()?.TenTapThe;
            var email = _context.NhanViens
                                             .Where(t => t.ID == NhanVienID)
                                             .Select(t => t.Email)
                                             .FirstOrDefault();

            if (email != null)
            {
                var tentapthe = new { id = email, Email = email };
                return Json(tentapthe); // Trả về dữ liệu dưới dạng JSON
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về số ngày công
            }
        }
        public IActionResult GetChucVu(int NhanVienID)
        {
            // Truy vấn nhân viên dựa trên NhanVienID
            var nhanVien = _context.NhanViens
                                    .Include(nv => nv.ChucVu) // Kết hợp bảng ChucVu để có thể truy cập tên chức vụ
                                    .FirstOrDefault(nv => nv.ID == NhanVienID);

            if (nhanVien != null)
            {
                // Tạo một object mới chứa ID và tên chức vụ
                var tenChucVu = new { id = nhanVien.ChucVu.ID, tenChucVu = nhanVien.ChucVu.TenChucVu };
                return Json(tenChucVu); 
            }
            else
            {
                return Json(null); // Trả về null nếu không tìm thấy thông tin về nhân viên
            }
        }

        // POST: NhanVien/ThemMoi

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemMoi( NhanVien nhanVien)
        {
            ViewData["ChucVuID"] = new SelectList(_context.ChucVus, "ID", "TenChucVu",nhanVien.ChucVuID);
            ViewData["ChuyenNganhID"] = new SelectList(_context.ChuyenNganhs, "ID", "TenChuyenNganh",nhanVien.ChuyenNganhID);
            ViewData["PhongBanID"] = new SelectList(_context.PhongBans, "ID", "TenPhongBan",nhanVien.PhongBanId);
            ViewData["TrinhDoID"] = new SelectList(_context.TrinhDos, "ID", "TenTrinhDo",nhanVien.TrinhDoID);

            if (ModelState.IsValid)
            {
                // Kiểm tra nếu MaNhanVien đã tồn tại
                var existingNhanVien = await _context.NhanViens
                    .FirstOrDefaultAsync(n => n.MaNhanVien == nhanVien.MaNhanVien);

                if (existingNhanVien != null)
                {
                    ModelState.AddModelError("MaNhanVien", "Mã nhân viên đã tồn tại. Vui lòng nhập mã khác!");
                    return View(nhanVien);
                }
                else
                {
                    if(nhanVien.HinhAnhNhanVien != null)
                    {
                        string uploadsDir = Path.Combine(_hostEnvironment.WebRootPath,"uploads");
                        string imageName = Guid.NewGuid().ToString() + "_" + nhanVien.HinhAnhNhanVien.Name;
                        string filePath = Path.Combine(uploadsDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await nhanVien.HinhAnhNhanVien.CopyToAsync(fs);
                        fs.Close();
                        nhanVien.HinhAnh = imageName;
                    }
                }

                // Lưu dữ liệu nếu mã nhân viên hợp lệ
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                TempData["seccess"] = "Thêm nhân viên thành công";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Model có một vài thứ bị lỗi";
                List<string> errors = new List<string>();
                foreach(var value in ModelState.Values)
                {
                    foreach(var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(nhanVien);
        }

        // GET: NhanVien/ChiTiet
        public async Task<IActionResult> ChiTiet(int id)
        {
            // Lấy nhân viên từ cơ sở dữ liệu
            var nhanVien = await _context.NhanViens
                .Include(nv => nv.ChucVu) // Kết hợp bảng Chức Vụ
                .Include(nv => nv.ChuyenNganh) // Kết hợp bảng Chuyên Ngành
                .Include(nv => nv.PhongBan) // Kết hợp bảng Phòng Ban
                .Include(nv => nv.TrinhDo) // Kết hợp bảng Trình Độ
                .FirstOrDefaultAsync(nv => nv.ID == id);

            if (nhanVien == null)
            {
                // Nếu không tìm thấy nhân viên, trả về lỗi 404
                return NotFound();
            }

            return View(nhanVien); // Truyền dữ liệu nhân viên vào view
        }


        // GET: NhanVien/Sua
        public async Task<IActionResult> Sua(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy nhân viên
            }

            ViewData["ChucVuID"] = new SelectList(_context.ChucVus, "ID", "TenChucVu", nhanVien.ChucVuID);
            ViewData["ChuyenNganhID"] = new SelectList(_context.ChuyenNganhs, "ID", "TenChuyenNganh", nhanVien.ChuyenNganhID);
            ViewData["PhongBanID"] = new SelectList(_context.PhongBans, "ID", "TenPhongBan", nhanVien.PhongBanId);
            ViewData["TrinhDoID"] = new SelectList(_context.TrinhDos, "ID", "TenTrinhDo", nhanVien.TrinhDoID);

            return View(nhanVien);
        }

        // POST: NhanVien/Sua
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sua(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.ID)
            {
                return NotFound(); // Kiểm tra ID có khớp không
            }

            ViewData["ChucVuID"] = new SelectList(_context.ChucVus, "ID", "TenChucVu", nhanVien.ChucVuID);
            ViewData["ChuyenNganhID"] = new SelectList(_context.ChuyenNganhs, "ID", "TenChuyenNganh", nhanVien.ChuyenNganhID);
            ViewData["PhongBanID"] = new SelectList(_context.PhongBans, "ID", "TenPhongBan", nhanVien.PhongBanId);
            ViewData["TrinhDoID"] = new SelectList(_context.TrinhDos, "ID", "TenTrinhDo", nhanVien.TrinhDoID);

            if (ModelState.IsValid)
            {
                var existingNhanVien = await _context.NhanViens.FindAsync(nhanVien.ID);
                if (existingNhanVien == null)
                {
                    return NotFound(); // Trả về lỗi nếu không tìm thấy nhân viên
                }

                // Kiểm tra nếu Mã Nhân Viên đã tồn tại
                var duplicateNhanVien = await _context.NhanViens
                    .FirstOrDefaultAsync(n => n.MaNhanVien == nhanVien.MaNhanVien && n.ID != nhanVien.ID);
                if (duplicateNhanVien != null)
                {
                    ModelState.AddModelError("MaNhanVien", "Mã nhân viên đã tồn tại. Vui lòng nhập mã khác!");
                    return View(nhanVien); // Trả về lỗi nếu mã nhân viên trùng
                }

                // Xử lý ảnh nếu có thay đổi
                if (nhanVien.HinhAnhNhanVien != null)
                {
                    string uploadsDir = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    string imageName = Guid.NewGuid().ToString() + "_" + nhanVien.HinhAnhNhanVien.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    // Xóa ảnh cũ nếu có
                    string oldfilePath = Path.Combine(uploadsDir, existingNhanVien.HinhAnh);
                    if (System.IO.File.Exists(oldfilePath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "Có lỗi khi xoá ảnh nhân viên: " + ex.Message);
                        }
                    }

                    // Lưu ảnh mới
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await nhanVien.HinhAnhNhanVien.CopyToAsync(fs);
                    }

                    // Cập nhật tên ảnh vào cơ sở dữ liệu
                    existingNhanVien.HinhAnh = imageName;
                }

                // Cập nhật thông tin nhân viên
                existingNhanVien.MaNhanVien = nhanVien.MaNhanVien;
                existingNhanVien.TenNhanVien = nhanVien.TenNhanVien;
                existingNhanVien.GioiTinh = nhanVien.GioiTinh;
                existingNhanVien.NoiO = nhanVien.NoiO;
                existingNhanVien.SDT = nhanVien.SDT;
                existingNhanVien.ChucVuID = nhanVien.ChucVuID;
                existingNhanVien.ChuyenNganhID = nhanVien.ChuyenNganhID;
                existingNhanVien.PhongBanId = nhanVien.PhongBanId;
                existingNhanVien.TrinhDoID = nhanVien.TrinhDoID;

                // Lưu cập nhật vào cơ sở dữ liệu
                _context.Update(existingNhanVien);
                await _context.SaveChangesAsync();

                TempData["success"] = "Cập nhật nhân viên thành công";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Có lỗi khi cập nhật nhân viên.";
            return View(nhanVien); // Trả về nếu model không hợp lệ
        }


        //NhanVien/Xoa
        public async Task<IActionResult> Xoa(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (!string.Equals(nhanVien.HinhAnh, "noname.jpg"))
            {
                string uploadsDir = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                string oldfilePath = Path.Combine(uploadsDir, nhanVien.HinhAnh);             
                if (System.IO.File.Exists(oldfilePath))
                {
                    System.IO.File.Delete(oldfilePath);
                }
            }
            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();
            TempData["seccess"] = "Xoá nhân viên thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}
