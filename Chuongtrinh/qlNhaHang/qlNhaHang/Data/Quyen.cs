using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Quyen
{
    public string MaQuyen { get; set; } = null!;

    public string? TenQuyen { get; set; }

    public virtual ICollection<LoaiTv> MaLoaiTvs { get; set; } = new List<LoaiTv>();
}
