using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class NguyenLieu
{
    public int IdNl { get; set; }

    public string? TenNl { get; set; }

    public decimal? Dongia { get; set; }

    public string? Donvitinh { get; set; }

    public virtual ICollection<Ctnk> Ctnks { get; set; } = new List<Ctnk>();

    public virtual ICollection<Ctxk> Ctxks { get; set; } = new List<Ctxk>();

    public virtual Kho? Kho { get; set; }

    public virtual ICollection<ThanhPhan> ThanhPhans { get; set; } = new List<ThanhPhan>();
}
