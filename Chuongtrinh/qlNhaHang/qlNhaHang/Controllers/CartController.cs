using Microsoft.AspNetCore.Mvc;
using qlNhaHang.Data;
using qlNhaHang.Helpers;
using qlNhaHang.ViewComponents;
using qlNhaHang.ViewModels;

namespace qlNhaHang.Controllers
{
    public class CartController : Controller
    {
        private readonly QlnhaHangContext db;

        public CartController(QlnhaHangContext context)
        {
            db = context;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(Mysetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var giohang = Cart;          
            var item = giohang.SingleOrDefault(p => p.MaMA == id);
            if (item == null)
            {
                var hanghoa = db.MonAns.SingleOrDefault(p => p.IdMonAn == id);
                if (hanghoa == null)
                {
                    return Redirect("MonAn/HomeNhaHang.cshtml");
                }
                else
                {
                    item = new CartItem
                    {
                        MaMA = hanghoa.IdMonAn,
                        TenMA = hanghoa.TenMon,
                        DonGia = (double)hanghoa.DonGia,
                        Hinh = hanghoa.Anhdaidien ?? string.Empty,
                        SoLuong = quantity
                    };
                    giohang.Add(item);
                }

            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(Mysetting.CART_KEY, giohang);
            return RedirectToAction("Index", "MonAn");
        }
        public IActionResult RemoveCart(int id)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaMA == id);
            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set(Mysetting.CART_KEY, giohang);
            }
            return RedirectToAction("Index");
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var giohang = Cart;
            var item = giohang.SingleOrDefault(p => p.MaMA == id);
            if (item != null)
            {
                item.SoLuong = quantity;
                HttpContext.Session.Set(Mysetting.CART_KEY, giohang);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            if(Cart.Count == 0)
            {
                return RedirectToAction("Index", "MonAn");
            }   
            return View(Cart);
        }
        
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            var hoadon = new HoaDon
            {
                HoTen = model.HoTen,
                SdtkhachHang = model.DienThoai,
                Email = model.Email,
                SoNguoi = model.SoNguoi,
                NgayHd = model.NgayBook,
                Trangthai = "Đã đặt bàn",
                GioNhanBan = model.GioNhanBan,
                Tongtien = model.Tongtien
            };
            db.Database.BeginTransaction();
            try
            {
                db.Database.CommitTransaction();
                db.Add(hoadon);
                db.SaveChanges();
                var cthds = new List<Cthd>();
                foreach (var item in Cart)
                {
                    cthds.Add(new Cthd
                    {
                        IdHoaDon = hoadon.IdHoaDon,
                        SoLuong = item.SoLuong,
                        Thanhtien = (decimal)item.ThanhTien,
                        IdMonAn = item.MaMA                     
                    });
                }
                db.AddRange(cthds);
                db.SaveChanges();

                HttpContext.Session.Set<List<CartItem>>(Mysetting.CART_KEY, new List<CartItem>());
                return View("Success");

            }
            catch{
                db.Database.RollbackTransaction();
            }
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
       
    }
}
