using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Ctxk
{
    public int IdXk { get; set; }

    public int IdNl { get; set; }

    public double? Soluong { get; set; }

    public virtual KhoNguyenLieu IdNlNavigation { get; set; } = null!;

    public virtual PhieuXk IdXkNavigation { get; set; } = null!;
}
