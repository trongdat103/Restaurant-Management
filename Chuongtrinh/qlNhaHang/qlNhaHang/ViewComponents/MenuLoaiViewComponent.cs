using Microsoft.AspNetCore.Mvc;
using qlNhaHang.Data;
using qlNhaHang.ViewModels;

namespace qlNhaHang.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly QlnhaHangContext db;
        public MenuLoaiViewComponent(QlnhaHangContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.LoaiMonAns.Select(lo => new MenuLoaiVM { IdLoaiMonAn =  lo.IdLoaiMonAn, TenLoai = lo.TenLoai });
            return View(data);
        }
    }
}
