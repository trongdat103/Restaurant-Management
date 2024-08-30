using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Voucher
{
    public int CodeVoucher { get; set; }

    public string? Mota { get; set; }

    public decimal? Phantram { get; set; }

    public string? LoaiMa { get; set; }

    public int? SoLuong { get; set; }

    public int? Diem { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
