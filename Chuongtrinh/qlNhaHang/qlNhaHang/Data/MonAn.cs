using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class MonAn
{
    public int IdMonAn { get; set; }

    public string? TenMon { get; set; }

    public decimal? DonGia { get; set; }

    public string? MoTa { get; set; }

    public string? IdLoaiMonAn { get; set; }

    public string? TrangThai { get; set; }

    public string? Anhdaidien { get; set; }

    public string? HinhAnh1 { get; set; }

    public string? HinhAnh2 { get; set; }

    public string? HinhAnh3 { get; set; }

    public decimal? GiaVon { get; set; }

    public virtual ICollection<Cthd> Cthds { get; set; } = new List<Cthd>();

    public virtual LoaiMonAn? IdLoaiMonAnNavigation { get; set; }

    public virtual ICollection<ThanhPhan> ThanhPhans { get; set; } = new List<ThanhPhan>();
}
