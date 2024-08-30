using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Kho
{
    public int IdNl { get; set; }

    public decimal? Slton { get; set; }

    public virtual NguyenLieu IdNlNavigation { get; set; } = null!;
}
