using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlNhaHang.Data;
using X.PagedList;

namespace qlNhaHang.Controllers
{
    public class MonAnController : Controller
    {
        private readonly QlnhaHangContext db;

        public MonAnController(QlnhaHangContext context)
        {
            db = context;
        }
        public IActionResult Index( int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.MonAns.AsNoTracking().Where(n => n.TrangThai == "Còn").OrderBy(x => x.TenMon); 
            PagedList<MonAn> lst = new PagedList<MonAn>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult MonAnTheoLoai(string? IdLoaiMonAn, int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var MonAnTheoLoai = db.MonAns.AsNoTracking().Where(x => x.IdLoaiMonAn == IdLoaiMonAn && x.TrangThai == "Còn").OrderBy(x => x.TenMon);
            PagedList<MonAn> lst = new PagedList<MonAn>(MonAnTheoLoai, pageNumber, pageSize);
            ViewBag.maloai = IdLoaiMonAn;
            return View(lst);
        }
        public IActionResult HomeNhaHang()
        {
           
            return View();
        }
        public IActionResult BookTable()
        {

            return View();
        }
    }
}
