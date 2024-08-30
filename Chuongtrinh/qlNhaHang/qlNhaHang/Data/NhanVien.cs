using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class NhanVien
{
    public int IdNv { get; set; }

    public string? TenNv { get; set; }

    public DateTime? NgayVl { get; set; }

    public string? Sdt { get; set; }

    public string? Chucvu { get; set; }

    public int? IdNd { get; set; }

    public string? Tinhtrang { get; set; }

    public virtual NguoiDung? IdNdNavigation { get; set; }

    public virtual ICollection<PhieuNk> PhieuNks { get; set; } = new List<PhieuNk>();

    public virtual ICollection<PhieuXk> PhieuXks { get; set; } = new List<PhieuXk>();
}
