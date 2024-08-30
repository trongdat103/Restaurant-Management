using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Ctnk
{
    public int IdNk { get; set; }

    public int IdNl { get; set; }

    public double? Soluong { get; set; }

    public decimal? Thanhtien { get; set; }

    public virtual PhieuNk IdNkNavigation { get; set; } = null!;

    public virtual KhoNguyenLieu IdNlNavigation { get; set; } = null!;
}
