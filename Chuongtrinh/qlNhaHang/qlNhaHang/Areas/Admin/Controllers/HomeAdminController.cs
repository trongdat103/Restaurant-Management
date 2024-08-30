using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlNhaHang.Areas.ViewModels;
using qlNhaHang.Data;
using qlNhaHang.Helpers;
using qlNhaHang.ViewComponents;
using qlNhaHang.ViewModels;
using X.PagedList;
namespace qlNhaHang.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        private readonly QlnhaHangContext db;
        public HomeAdminController(QlnhaHangContext context)
        {
            db = context;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhMucMonAn")]
        public IActionResult DanhMucMonAn(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.MonAns.AsNoTracking().Where(n => n.TrangThai == "Còn").OrderBy(x => x.TenMon);
            PagedList<MonAn> lst = new PagedList<MonAn>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        [HttpGet]
        [Route("ThemMonAn")]
        public IActionResult ThemMonAn()
        {
            ViewBag.MaLoaiMA = new SelectList(db.LoaiMonAns.ToList(), "IdLoaiMonAn", "TenLoai");
            return View();
        }

        [HttpPost]
        [Route("ThemMonAn")]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMonAn(MonAn MA, IFormFile Anhdaidien)
        {
            if (ModelState.IsValid)
            {
                // Xử lý file upload
                if (Anhdaidien != null && Anhdaidien.Length > 0)
                {
                    // Đường dẫn thư mục lưu ảnh
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ImageProduct");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    // Tạo tên file duy nhất
                    var fileName = Path.GetFileNameWithoutExtension(Anhdaidien.FileName);
                    var extension = Path.GetExtension(Anhdaidien.FileName);
                    var newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";

                    var filePath = Path.Combine(uploads, newFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Anhdaidien.CopyTo(fileStream);
                    }

                    // Lưu tên file vào thuộc tính AnhDaiDien
                    MA.Anhdaidien = newFileName;
                }

                MA.TrangThai = "Còn";
                db.MonAns.Add(MA);
                db.SaveChanges();
                return RedirectToAction("DanhMucMonAn");
            }
            return View(MA);
        }
        [HttpGet]
        [Route("ChinhSuaMonAn/{IdMonAn}")]
        public IActionResult ChinhSuaMonAn(int IdMonAn)
        {
            var monAn = db.MonAns.SingleOrDefault(m => m.IdMonAn == IdMonAn);
            if (monAn == null)
            {
                return NotFound();
            }

            ViewBag.MaLoaiMA = new SelectList(db.LoaiMonAns.ToList(), "IdLoaiMonAn", "TenLoai");
            return View(monAn);
        }

        [HttpPost]
        [Route("ChinhSuaMonAn/{IdMonAn}")]
        [ValidateAntiForgeryToken]
        public IActionResult ChinhSuaMonAn(int IdMonAn, MonAn MA, IFormFile? AnhdaidienFile)
        {
            if (ModelState.IsValid)
            {                          // Cập nhật các thuộc tính của món ăn từ form

                // Xử lý file upload
                if (AnhdaidienFile != null && AnhdaidienFile.Length > 0)
                {
                    // Đường dẫn thư mục lưu ảnh
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ImageProduct");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    // Tạo tên file duy nhất
                    var fileName = Path.GetFileNameWithoutExtension(AnhdaidienFile.FileName);
                    var extension = Path.GetExtension(AnhdaidienFile.FileName);
                    var newFileName = $"{fileName}_{DateTime.Now.Ticks}{extension}";

                    var filePath = Path.Combine(uploads, newFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        AnhdaidienFile.CopyTo(fileStream);
                    }

                    // Lưu tên file vào thuộc tính AnhDaiDien
                    MA.Anhdaidien = newFileName;
                }
                MA.TrangThai = "Còn";
                db.Entry(MA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucMonAn");
            }

            // Đặt lại giá trị ViewBag.MaLoaiMA để hiển thị dropdown list
            ViewBag.MaLoaiMA = new SelectList(db.LoaiMonAns.ToList(), "IdLoaiMonAn", "TenLoai");
            // Lấy lại ảnh cũ nếu không có file mới được upload        
            return View(MA);
        }
        [Route("XoaMonAn")]
        public IActionResult XoaMonAn(int IdMonAn)
        {
            var monAn = db.MonAns.SingleOrDefault(m => m.IdMonAn == IdMonAn);
            if (monAn == null)
            {
                return NotFound();
            }

            monAn.TrangThai = "Hết";
            db.Entry(monAn).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DanhMucMonAn");
        }
        [Route("LichDatBan")]
        public IActionResult LichDatBan(int? page)
        {
            var DanhSachBan = db.Bans.AsNoTracking().OrderBy(x => x.IdBan);
            ViewBag.Tenban = DanhSachBan;
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var Lichdatban = db.HoaDons.AsNoTracking().Where(n => n.Trangthai == "Đã đặt bàn").OrderBy(x => x.NgayHd);
            PagedList<HoaDon> lst = new PagedList<HoaDon>(Lichdatban, pageNumber, pageSize);
            return View(lst);
        }
        [Route("DanhSachBan")]
        public IActionResult DanhSachBan(int IdTang)
        {
            // Truy vấn danh sách bàn với điều kiện IdTang bằng IdTang truyền vào
            var SoDoBan = db.Bans.AsNoTracking()
                                 .Where(n => n.IdTang == IdTang)
                                 .OrderBy(x => x.IdBan)
                                 .ToList();
            // Trả về view với danh sách bàn đã lọc
            return View(SoDoBan);
        }
        [Route("DanhSachBanHD")]
        public IActionResult DanhSachBanHD(int ID_HoaDon)
        {
            HttpContext.Session.SetInt32("ID_HoaDon", ID_HoaDon);
            List<Ban> DanhSachBan;
            // Lấy hóa đơn dựa trên ID_HoaDon
            var HoaDonBan = db.HoaDons.SingleOrDefault(p => p.IdHoaDon == ID_HoaDon);

            // Kiểm tra nếu hóa đơn tồn tại
            if (HoaDonBan != null)
            {
                // Lấy IdBan từ HoaDonBan
                var idBan = HoaDonBan.IdBan;
                if(idBan != null)
                {
                    // Lấy idTang từ bảng Ban dựa trên IdBan
                    var idtang = db.Bans
                                   .Where(p => p.IdBan == idBan)
                                   .Select(x => x.IdTang)
                                   .SingleOrDefault();

                    // Truy vấn danh sách bàn với điều kiện IdTang bằng idtang từ hóa đơn
                    DanhSachBan = db.Bans.AsNoTracking()
                                         .Where(n => n.IdTang == idtang)
                                         .OrderBy(x => x.IdBan)
                                         .ToList();                 
                }
                else
                {
                    int idtangnull = 1;
                    DanhSachBan = db.Bans.AsNoTracking()
                                         .Where(n => n.IdTang == idtangnull)
                                         .OrderBy(x => x.IdBan)
                                         .ToList();
                }               
            }
            else
            {
                // Nếu không tìm thấy hóa đơn, trả về NotFound
                return NotFound();
            }
            ViewBag.ID_HoaDon = ID_HoaDon;
            // Trả về view với danh sách bàn đã lọc
            return View(DanhSachBan);
        }
        [Route("DanhSachBanHDTang")]
        public IActionResult DanhSachBanHDTang(int ID_HoaDon, int IdTang)
        {
            // Truy vấn danh sách bàn với điều kiện IdTang bằng IdTang truyền vào          
            var DanhSachBan = db.Bans.AsNoTracking()
                                 .Where(n => n.IdTang == IdTang)
                                 .OrderBy(x => x.IdBan)
                                 .ToList();
            // Gán ID_HoaDon vào ViewBag để truyền sang view
            ViewBag.ID_HoaDonTang = ID_HoaDon;
            // Trả về view với danh sách bàn đã lọc
            return View(DanhSachBan);
        }
        [Route("Addban")]
        public IActionResult Addban(int IdHoaDon, int IdBan)
        {
            var trangthai = db.Bans.SingleOrDefault(p => p.IdBan == IdBan);
            trangthai.Trangthai = "Đã đặt trước";
            db.Entry(trangthai).State = EntityState.Modified;
            var Hoadon = db.HoaDons.SingleOrDefault(p => p.IdHoaDon == IdHoaDon);
            if (Hoadon.IdBan != null)
            {
                var xoatrangthai = db.Bans.SingleOrDefault(p => p.IdBan == Hoadon.IdBan);
                xoatrangthai.Trangthai = null;
                db.Entry(xoatrangthai).State = EntityState.Modified;
                db.SaveChanges();
            }
            Hoadon.IdBan = IdBan;
            db.Entry(Hoadon).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("LichDatBan");
        }
        [Route("MonAnHoaDon")]
        public async Task<IActionResult> MonAnHoaDon(int ID_HoaDon)
        {

            var chiTietHoaDons = (await (from ct in db.Cthds
                                         join ma in db.MonAns on ct.IdMonAn equals ma.IdMonAn
                                         where ct.IdHoaDon == ID_HoaDon
                                         select new ChiTietHoaDonViewModel
                                         {
                                             IdMonAn = ct.IdMonAn,
                                             TenMonAn = ma.TenMon,
                                             DonGia = ma.DonGia,
                                             SoLuong = ct.SoLuong,
                                             Anhdaidien = ma.Anhdaidien,
                                             Thanhtien = ct.Thanhtien
                                         }).ToListAsync());
            var lstsanpham = db.MonAns.AsNoTracking().Where(n => n.TrangThai == "Còn").OrderBy(x => x.TenMon);
            ViewBag.Danhsachsp = lstsanpham;
            ViewBag.ID_HoaDonMA = ID_HoaDon;
            return View(chiTietHoaDons);

        }
        [Route("CapNhatChiTietHD")]
        public IActionResult CapNhatChiTietHD(int idMonAn, int IdHoaDon, int quantity = 1)
        {
            // Tìm chi tiết hóa đơn đã tồn tại
            var chiTietHD = db.Cthds.SingleOrDefault(p => p.IdHoaDon == IdHoaDon && p.IdMonAn == idMonAn);

            // Lấy đơn giá từ bảng MonAns
            var donGia = db.MonAns.Where(p => p.IdMonAn == idMonAn)
                                   .Select(p => p.DonGia)
                                   .SingleOrDefault();

            // Kiểm tra đơn giá hợp lệ
            if (donGia <= 0)
            {
                // Xử lý khi không tìm thấy đơn giá hợp lệ
                // Có thể trả về lỗi hoặc thông báo cho người dùng
                return BadRequest("Đơn giá không hợp lệ.");
            }

            if (chiTietHD != null)
            {
                // Cập nhật số lượng và thành tiền
                chiTietHD.SoLuong += quantity;
                chiTietHD.Thanhtien = chiTietHD.SoLuong * donGia;

                // Cập nhật trạng thái của thực thể
                db.Entry(chiTietHD).State = EntityState.Modified;
            }
            else
            {
                // Tạo chi tiết hóa đơn mới
                var newChiTietHD = new Cthd
                {
                    IdHoaDon = IdHoaDon,
                    IdMonAn = idMonAn,
                    SoLuong = quantity,
                    Thanhtien = quantity * donGia
                };

                // Thêm chi tiết hóa đơn mới vào cơ sở dữ liệu
                db.Cthds.Add(newChiTietHD);
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();

            // Chuyển hướng đến hành động MonAnHoaDon và truyền IdHoaDon
            return RedirectToAction("MonAnHoaDon", new { ID_HoaDon = IdHoaDon });
        }
        [Route("XoaMonAnHD")]
        public IActionResult XoaMonAnHD(int IdMonAnHD, int IdHoaDon)
        {
            if (IdMonAnHD <= 0 || IdHoaDon <= 0)
            {
                return BadRequest("Thông tin món ăn hoặc hóa đơn không hợp lệ.");
            }

            try
            {
                var monAnHD = db.Cthds.SingleOrDefault(m => m.IdMonAn == IdMonAnHD && m.IdHoaDon == IdHoaDon);
                if (monAnHD != null)
                {
                    db.Cthds.Remove(monAnHD);
                    db.SaveChanges();
                }
                return RedirectToAction("MonAnHoaDon", new { ID_HoaDon = IdHoaDon });
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                // _logger.LogError(ex, "Error deleting item from order.");
                return StatusCode(500, "Có lỗi xảy ra trong quá trình xóa món ăn khỏi hóa đơn.");
            }
        }
        [Route("UpdateSl")]
        public IActionResult UpdateSl(int IdMonAnHD, int IdHoaDon, int quantity)
        {
            var chiTietHD = db.Cthds.SingleOrDefault(p => p.IdHoaDon == IdHoaDon && p.IdMonAn == IdMonAnHD);

            // Lấy đơn giá từ bảng MonAns
            var donGia = db.MonAns.Where(p => p.IdMonAn == IdMonAnHD)
                                   .Select(p => p.DonGia)
                                   .SingleOrDefault();

            // Kiểm tra đơn giá hợp lệ
            if (donGia <= 0)
            {
                // Xử lý khi không tìm thấy đơn giá hợp lệ
                // Có thể trả về lỗi hoặc thông báo cho người dùng
                return BadRequest("Đơn giá không hợp lệ.");
            }

            if (chiTietHD != null)
            {
                // Cập nhật số lượng và thành tiền
                chiTietHD.SoLuong = quantity;
                chiTietHD.Thanhtien = chiTietHD.SoLuong * donGia;

                // Cập nhật trạng thái của thực thể
                db.Entry(chiTietHD).State = EntityState.Modified;
            }
            else
            {
                // Tạo chi tiết hóa đơn mới
                return RedirectToAction("MonAnHoaDon", new { ID_HoaDon = IdHoaDon });
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();
            // Chuyển hướng đến hành động MonAnHoaDon và truyền IdHoaDon
            return RedirectToAction("MonAnHoaDon", new { ID_HoaDon = IdHoaDon });
        }
        //trạng thái nhận bàn
        [Route("UpdateNhanBan")]
        public IActionResult UpdateNhanBan(int ID_HoaDon)
        {
            var Hoadon = db.HoaDons.SingleOrDefault(p => p.IdHoaDon == ID_HoaDon);
            if (Hoadon != null)
            {
                // Cập nhật số lượng và thành tiền
                Hoadon.Trangthai = "Đã nhận bàn";
                // Cập nhật trạng thái của thực thể
                db.Entry(Hoadon).State = EntityState.Modified;
            }
            else
            {
                // Tạo chi tiết hóa đơn mới
                return RedirectToAction("LichDatBan");
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();
            // Chuyển hướng đến hành động MonAnHoaDon và truyền IdHoaDon
            return RedirectToAction("LichDatBan");
        }

        [Route("HuyLich")]
        public IActionResult HuyLich(int ID_HoaDon)
        {
            var Hoadon = db.HoaDons.SingleOrDefault(p => p.IdHoaDon == ID_HoaDon);
            if (Hoadon != null)
            {
                if (Hoadon.IdBan != null)
                {
                    var idban = db.Bans.SingleOrDefault(p => p.IdBan == Hoadon.IdBan);
                    if (idban != null)
                    {
                        idban.Trangthai = null;
                        db.Entry(idban).State = EntityState.Modified;
                    }
                    else
                    {
                        return RedirectToAction("LichDatBan");
                    }
                }
                // Cập nhật số lượng và thành tiền
                Hoadon.Trangthai = "Đã hủy";
                // Cập nhật trạng thái của thực thể
                db.Entry(Hoadon).State = EntityState.Modified;
            }
            else
            {
                // Tạo chi tiết hóa đơn mới
                return RedirectToAction("LichDatBan");
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            db.SaveChanges();
            // Chuyển hướng đến hành động MonAnHoaDon và truyền IdHoaDon
            return RedirectToAction("LichDatBan");
        }

        [Route("DonHienThoi")]
        public IActionResult DonHienThoi(int? page)
        {
            var DanhSachBan = db.Bans.AsNoTracking().OrderBy(x => x.IdBan);
            var Tonghoadon = db.Cthds.AsNoTracking();
            ViewBag.Tongtien = Tonghoadon.ToList();
            ViewBag.Tenban = DanhSachBan;
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var DonHienThoi = db.HoaDons.AsNoTracking().Where(n => n.Trangthai == "Đã nhận bàn").OrderBy(x => x.NgayHd);
            PagedList<HoaDon> lst = new PagedList<HoaDon>(DonHienThoi, pageNumber, pageSize);
            return View(lst);
        }
    }
}
