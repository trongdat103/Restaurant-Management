using Microsoft.AspNetCore.Mvc;
using qlNhaHang.Areas.ViewModels;
using qlNhaHang.Data;

namespace qlNhaHang.Areas.ViewComponents
{
    public class MenuTangHDViewComponent : ViewComponent
    {
        private readonly QlnhaHangContext db;
        public MenuTangHDViewComponent(QlnhaHangContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Tangs.Select(lo => new TangViewModel { IdTang = lo.IdTang, TenTang = lo.TenTang });
            return View(data);
        }
    }
}
