using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class DanhGium
{
    public int MaDanhGia { get; set; }

    public int? IdNd { get; set; }

    public string? Binhluan { get; set; }

    public int? SaoDanhGia { get; set; }

    public virtual NguoiDung? IdNdNavigation { get; set; }
}
