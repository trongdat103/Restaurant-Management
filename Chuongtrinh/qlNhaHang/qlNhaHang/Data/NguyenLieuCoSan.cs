using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class NguyenLieuCoSan
{
    public int IdnguyenLieuCoSan { get; set; }

    public string? TenNguyenLieu { get; set; }

    public double? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public string? DonViTinh { get; set; }

    public int? IdNl { get; set; }

    public virtual KhoNguyenLieu? IdNlNavigation { get; set; }

    public virtual ICollection<ThanhPhan> ThanhPhans { get; set; } = new List<ThanhPhan>();
}
