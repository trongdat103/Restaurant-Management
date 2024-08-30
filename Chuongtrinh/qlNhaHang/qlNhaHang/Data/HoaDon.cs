using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class HoaDon
{
    public int IdHoaDon { get; set; }

    public int? IdBan { get; set; }

    public DateOnly? NgayHd { get; set; }

    public decimal? TienMonAn { get; set; }

    public int? CodeVoucher { get; set; }

    public decimal? TienGiam { get; set; }

    public decimal? Tongtien { get; set; }

    public string? Trangthai { get; set; }

    public int? SoNguoi { get; set; }

    public string? SdtkhachHang { get; set; }

    public string? HoTen { get; set; }

    public int? IdKh { get; set; }

    public string? Email { get; set; }

    public string? GioNhanBan { get; set; }

    public virtual Voucher? CodeVoucherNavigation { get; set; }

    public virtual ICollection<Cthd> Cthds { get; set; } = new List<Cthd>();

    public virtual Ban? IdBanNavigation { get; set; }

    public virtual KhachHang? IdKhNavigation { get; set; }
}
