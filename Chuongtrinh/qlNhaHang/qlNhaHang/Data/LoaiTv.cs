using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class LoaiTv
{
    public int MaLoaiTv { get; set; }

    public string? TenLoaiTv { get; set; }

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();

    public virtual ICollection<Quyen> MaQuyens { get; set; } = new List<Quyen>();
}
