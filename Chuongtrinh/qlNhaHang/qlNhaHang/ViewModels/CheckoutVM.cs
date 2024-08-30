namespace qlNhaHang.ViewModels
{
    public class CheckoutVM
    {
        public string HoTen {  get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public int SoNguoi { get; set; }
        public DateOnly NgayBook { get; set; }
        public string GioNhanBan { get; set; }
        public decimal? Tongtien { get; set; }
    }
}
