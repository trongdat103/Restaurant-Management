using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class PhieuXk
{
    public int IdXk { get; set; }

    public int? IdNv { get; set; }

    public DateTime? NgayXk { get; set; }

    public virtual ICollection<Ctxk> Ctxks { get; set; } = new List<Ctxk>();

    public virtual NhanVien? IdNvNavigation { get; set; }
}
