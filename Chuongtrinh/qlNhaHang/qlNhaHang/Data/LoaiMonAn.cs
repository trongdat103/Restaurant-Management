using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class LoaiMonAn
{
    public string IdLoaiMonAn { get; set; } = null!;

    public string? TenLoai { get; set; }

    public virtual ICollection<MonAn> MonAns { get; set; } = new List<MonAn>();
}
