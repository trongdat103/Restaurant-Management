using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class PhieuNk
{
    public int IdNk { get; set; }

    public int? IdNv { get; set; }

    public DateTime? NgayNk { get; set; }

    public decimal? Tongtien { get; set; }

    public virtual ICollection<Ctnk> Ctnks { get; set; } = new List<Ctnk>();

    public virtual NhanVien? IdNvNavigation { get; set; }
}
