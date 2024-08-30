using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class KhachHang
{
    public int IdKh { get; set; }

    public string? TenKh { get; set; }

    public DateTime? Ngaythamgia { get; set; }

    public decimal? Doanhso { get; set; }

    public decimal? Diemtichluy { get; set; }

    public int? IdNd { get; set; }

    public string? SdtkhachHang { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual NguoiDung? IdNdNavigation { get; set; }
}
