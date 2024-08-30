using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class Tang
{
    public int IdTang { get; set; }

    public string? TenTang { get; set; }

    public virtual ICollection<Ban> Bans { get; set; } = new List<Ban>();
}
