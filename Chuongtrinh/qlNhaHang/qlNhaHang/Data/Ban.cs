using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Ban
{
    public int IdBan { get; set; }

    public string? TenBan { get; set; }

    public string? Vitri { get; set; }

    public string? Trangthai { get; set; }

    public int? IdTang { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual Tang? IdTangNavigation { get; set; }
}
