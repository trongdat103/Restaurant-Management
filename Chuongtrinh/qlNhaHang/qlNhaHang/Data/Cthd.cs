using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Cthd
{
    public int IdHoaDon { get; set; }

    public int IdMonAn { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Thanhtien { get; set; }

    public virtual HoaDon IdHoaDonNavigation { get; set; } = null!;

    public virtual MonAn IdMonAnNavigation { get; set; } = null!;
}
