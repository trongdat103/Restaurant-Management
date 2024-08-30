namespace qlNhaHang.ViewComponents
{
    public class CartItem
    {
        public int MaMA { get; set; }
        public string Hinh { get; set; }
        public string TenMA { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
