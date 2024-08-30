using System;
using System.Collections.Generic;

namespace qlNhaHang.Data;

public partial class NguoiDung
{
    public int IdNd { get; set; }

    public string Username { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public string? VerifyCode { get; set; }

    public string? Trangthai { get; set; }

    public string? Vaitro { get; set; }

    public int? MaLoaiTv { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();

    public virtual LoaiTv? MaLoaiTvNavigation { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
