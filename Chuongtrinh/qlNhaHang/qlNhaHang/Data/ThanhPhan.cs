using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class ThanhPhan
{
    public int IdMonAn { get; set; }

    public int IdnguyenLieuCs { get; set; }

    public double? DinhLuong { get; set; }

    public decimal? UocGia { get; set; }

    public virtual MonAn IdMonAnNavigation { get; set; } = null!;

    public virtual NguyenLieuCoSan IdnguyenLieuCsNavigation { get; set; } = null!;
}
