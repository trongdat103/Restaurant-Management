using Microsoft.AspNetCore.Mvc;
using qlNhaHang.Helpers;
using qlNhaHang.ViewModels;

namespace qlNhaHang.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(Mysetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien)

            });
        }
    }
}
