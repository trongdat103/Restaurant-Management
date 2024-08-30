using Microsoft.AspNetCore.Mvc;
using qlNhaHang.Areas.ViewModels;
using qlNhaHang.Data;
using qlNhaHang.ViewModels;

namespace qlNhaHang.Areas.ViewComponents
{
    public class MenuTangViewComponent : ViewComponent
    {
        private readonly QlnhaHangContext db;
        public MenuTangViewComponent(QlnhaHangContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Tangs.Select(lo => new TangViewModel { IdTang = lo.IdTang, TenTang = lo.TenTang });
            return View(data);
        }
    }
}
