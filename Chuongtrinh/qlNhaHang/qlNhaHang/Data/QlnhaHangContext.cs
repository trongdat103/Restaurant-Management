using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace qlNhaHang.Data;

public partial class QlnhaHangContext : DbContext
{
    public QlnhaHangContext()
    {
    }

    public QlnhaHangContext(DbContextOptions<QlnhaHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<Cthd> Cthds { get; set; }

    public virtual DbSet<Ctnk> Ctnks { get; set; }

    public virtual DbSet<Ctxk> Ctxks { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhoNguyenLieu> KhoNguyenLieus { get; set; }

    public virtual DbSet<LoaiMonAn> LoaiMonAns { get; set; }

    public virtual DbSet<LoaiTv> LoaiTvs { get; set; }

    public virtual DbSet<MonAn> MonAns { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NguyenLieuCoSan> NguyenLieuCoSans { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhieuNk> PhieuNks { get; set; }

    public virtual DbSet<PhieuXk> PhieuXks { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<Tang> Tangs { get; set; }

    public virtual DbSet<ThanhPhan> ThanhPhans { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=HShop");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.IdBan).HasName("PK__Ban__142B84F9C372D429");

            entity.ToTable("Ban");

            entity.Property(e => e.IdBan).HasColumnName("ID_Ban");
            entity.Property(e => e.IdTang).HasColumnName("ID_Tang");
            entity.Property(e => e.TenBan).HasMaxLength(250);
            entity.Property(e => e.Trangthai).HasMaxLength(250);
            entity.Property(e => e.Vitri).HasMaxLength(250);

            entity.HasOne(d => d.IdTangNavigation).WithMany(p => p.Bans)
                .HasForeignKey(d => d.IdTang)
                .HasConstraintName("FK__Ban__ID_Tang__5441852A");
        });

        modelBuilder.Entity<Cthd>(entity =>
        {
            entity.HasKey(e => new { e.IdHoaDon, e.IdMonAn }).HasName("PK__CTHD__C9AEED644F14C2EE");

            entity.ToTable("CTHD");

            entity.Property(e => e.IdHoaDon).HasColumnName("ID_HoaDon");
            entity.Property(e => e.IdMonAn).HasColumnName("ID_MonAn");
            entity.Property(e => e.Thanhtien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdHoaDonNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.IdHoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHD__ID_HoaDon__5DCAEF64");

            entity.HasOne(d => d.IdMonAnNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.IdMonAn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHD__ID_MonAn__5EBF139D");
        });

        modelBuilder.Entity<Ctnk>(entity =>
        {
            entity.HasKey(e => new { e.IdNk, e.IdNl });

            entity.ToTable("CTNK");

            entity.Property(e => e.IdNk).HasColumnName("ID_NK");
            entity.Property(e => e.IdNl).HasColumnName("ID_NL");
            entity.Property(e => e.Thanhtien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdNkNavigation).WithMany(p => p.Ctnks)
                .HasForeignKey(d => d.IdNk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTNK_PhieuNK");

            entity.HasOne(d => d.IdNlNavigation).WithMany(p => p.Ctnks)
                .HasForeignKey(d => d.IdNl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CTNK_NguyenLieu");
        });

        modelBuilder.Entity<Ctxk>(entity =>
        {
            entity.HasKey(e => new { e.IdXk, e.IdNl }).HasName("PK__CTXK__43D46F2201BE6348");

            entity.ToTable("CTXK");

            entity.Property(e => e.IdXk).HasColumnName("ID_XK");
            entity.Property(e => e.IdNl).HasColumnName("ID_NL");

            entity.HasOne(d => d.IdNlNavigation).WithMany(p => p.Ctxks)
                .HasForeignKey(d => d.IdNl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTXK__ID_NL__4BAC3F29");

            entity.HasOne(d => d.IdXkNavigation).WithMany(p => p.Ctxks)
                .HasForeignKey(d => d.IdXk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTXK__ID_XK__4AB81AF0");
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.MaDanhGia);

            entity.Property(e => e.IdNd).HasColumnName("ID_ND");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.IdNd)
                .HasConstraintName("FK_DanhGia_NguoiDung");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.IdHoaDon).HasName("PK__HoaDon__14AFCFC52ED34183");

            entity.ToTable("HoaDon");

            entity.Property(e => e.IdHoaDon).HasColumnName("ID_HoaDon");
            entity.Property(e => e.CodeVoucher).HasColumnName("Code_Voucher");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.GioNhanBan).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(150);
            entity.Property(e => e.IdBan).HasColumnName("ID_Ban");
            entity.Property(e => e.IdKh).HasColumnName("ID_KH");
            entity.Property(e => e.NgayHd).HasColumnName("NgayHD");
            entity.Property(e => e.SdtkhachHang)
                .HasMaxLength(24)
                .HasColumnName("SDTKhachHang");
            entity.Property(e => e.TienGiam).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TienMonAn).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tongtien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Trangthai).HasMaxLength(250);

            entity.HasOne(d => d.CodeVoucherNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.CodeVoucher)
                .HasConstraintName("FK__HoaDon__Code_Vou__5AEE82B9");

            entity.HasOne(d => d.IdBanNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdBan)
                .HasConstraintName("FK__HoaDon__ID_Ban__59FA5E80");

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK_HoaDon_KhachHang");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.IdKh);

            entity.ToTable("KhachHang");

            entity.Property(e => e.IdKh).HasColumnName("ID_KH");
            entity.Property(e => e.Diemtichluy).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Doanhso).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdNd).HasColumnName("ID_ND");
            entity.Property(e => e.Ngaythamgia).HasColumnType("datetime");
            entity.Property(e => e.SdtkhachHang)
                .HasMaxLength(50)
                .HasColumnName("SDTKhachHang");
            entity.Property(e => e.TenKh)
                .HasMaxLength(250)
                .HasColumnName("TenKH");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.IdNd)
                .HasConstraintName("FK_KhachHang_NguoiDung");
        });

        modelBuilder.Entity<KhoNguyenLieu>(entity =>
        {
            entity.HasKey(e => e.IdNl).HasName("PK__NguyenLi__8B63E06D5CC76061");

            entity.ToTable("KhoNguyenLieu");

            entity.Property(e => e.IdNl).HasColumnName("ID_NL");
            entity.Property(e => e.Dongia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Donvitinh).HasMaxLength(250);
            entity.Property(e => e.Slton).HasColumnName("SLTon");
            entity.Property(e => e.TenNl)
                .HasMaxLength(250)
                .HasColumnName("TenNL");
        });

        modelBuilder.Entity<LoaiMonAn>(entity =>
        {
            entity.HasKey(e => e.IdLoaiMonAn);

            entity.ToTable("LoaiMonAn");

            entity.Property(e => e.IdLoaiMonAn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_LoaiMonAn");
            entity.Property(e => e.TenLoai).HasMaxLength(250);
        });

        modelBuilder.Entity<LoaiTv>(entity =>
        {
            entity.HasKey(e => e.MaLoaiTv);

            entity.ToTable("LoaiTV");

            entity.Property(e => e.MaLoaiTv).HasColumnName("MaLoaiTV");
            entity.Property(e => e.TenLoaiTv)
                .HasMaxLength(50)
                .HasColumnName("TenLoaiTV");
        });

        modelBuilder.Entity<MonAn>(entity =>
        {
            entity.HasKey(e => e.IdMonAn);

            entity.ToTable("MonAn");

            entity.Property(e => e.IdMonAn).HasColumnName("ID_MonAn");
            entity.Property(e => e.Anhdaidien).HasMaxLength(200);
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.GiaVon).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.HinhAnh1).HasMaxLength(200);
            entity.Property(e => e.HinhAnh2).HasMaxLength(200);
            entity.Property(e => e.HinhAnh3).HasMaxLength(200);
            entity.Property(e => e.IdLoaiMonAn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ID_LoaiMonAn");
            entity.Property(e => e.TenMon).HasMaxLength(150);
            entity.Property(e => e.TrangThai).HasMaxLength(20);

            entity.HasOne(d => d.IdLoaiMonAnNavigation).WithMany(p => p.MonAns)
                .HasForeignKey(d => d.IdLoaiMonAn)
                .HasConstraintName("FK_MonAn_LoaiMonAn");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.IdNd);

            entity.ToTable("NguoiDung");

            entity.Property(e => e.IdNd).HasColumnName("ID_ND");
            entity.Property(e => e.MaLoaiTv).HasColumnName("MaLoaiTV");
            entity.Property(e => e.Matkhau).HasMaxLength(250);
            entity.Property(e => e.Trangthai).HasMaxLength(210);
            entity.Property(e => e.Username).HasMaxLength(250);
            entity.Property(e => e.Vaitro).HasMaxLength(250);
            entity.Property(e => e.VerifyCode).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiTvNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaLoaiTv)
                .HasConstraintName("FK_NguoiDung_LoaiTV");
        });

        modelBuilder.Entity<NguyenLieuCoSan>(entity =>
        {
            entity.HasKey(e => e.IdnguyenLieuCoSan);

            entity.ToTable("NguyenLieuCoSan");

            entity.Property(e => e.IdnguyenLieuCoSan).HasColumnName("IDNguyenLieuCoSan");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.DonViTinh).HasMaxLength(250);
            entity.Property(e => e.IdNl).HasColumnName("ID_NL");
            entity.Property(e => e.TenNguyenLieu).HasMaxLength(200);

            entity.HasOne(d => d.IdNlNavigation).WithMany(p => p.NguyenLieuCoSans)
                .HasForeignKey(d => d.IdNl)
                .HasConstraintName("FK_NguyenLieuCoSan_KhoNguyenLieu");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.IdNv);

            entity.ToTable("NhanVien");

            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.Chucvu).HasMaxLength(50);
            entity.Property(e => e.IdNd).HasColumnName("ID_ND");
            entity.Property(e => e.NgayVl)
                .HasColumnType("datetime")
                .HasColumnName("NgayVL");
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
            entity.Property(e => e.Tinhtrang).HasMaxLength(20);

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.IdNd)
                .HasConstraintName("FK_NhanVien_NguoiDung");
        });

        modelBuilder.Entity<PhieuNk>(entity =>
        {
            entity.HasKey(e => e.IdNk);

            entity.ToTable("PhieuNK");

            entity.Property(e => e.IdNk).HasColumnName("ID_NK");
            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.NgayNk)
                .HasColumnType("datetime")
                .HasColumnName("NgayNK");
            entity.Property(e => e.Tongtien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.PhieuNks)
                .HasForeignKey(d => d.IdNv)
                .HasConstraintName("FK_PhieuNK_NhanVien");
        });

        modelBuilder.Entity<PhieuXk>(entity =>
        {
            entity.HasKey(e => e.IdXk);

            entity.ToTable("PhieuXK");

            entity.Property(e => e.IdXk).HasColumnName("ID_XK");
            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.NgayXk)
                .HasColumnType("datetime")
                .HasColumnName("NgayXK");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.PhieuXks)
                .HasForeignKey(d => d.IdNv)
                .HasConstraintName("FK_PhieuXK_NhanVien");
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.MaQuyen);

            entity.ToTable("Quyen");

            entity.Property(e => e.MaQuyen).HasMaxLength(50);
            entity.Property(e => e.TenQuyen).HasMaxLength(50);

            entity.HasMany(d => d.MaLoaiTvs).WithMany(p => p.MaQuyens)
                .UsingEntity<Dictionary<string, object>>(
                    "LoaiTvQuyen",
                    r => r.HasOne<LoaiTv>().WithMany()
                        .HasForeignKey("MaLoaiTv")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LoaiTV_Quyen_LoaiTV"),
                    l => l.HasOne<Quyen>().WithMany()
                        .HasForeignKey("MaQuyen")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LoaiTV_Quyen_Quyen"),
                    j =>
                    {
                        j.HasKey("MaQuyen", "MaLoaiTv");
                        j.ToTable("LoaiTV_Quyen");
                        j.IndexerProperty<string>("MaQuyen").HasMaxLength(50);
                        j.IndexerProperty<int>("MaLoaiTv").HasColumnName("MaLoaiTV");
                    });
        });

        modelBuilder.Entity<Tang>(entity =>
        {
            entity.HasKey(e => e.IdTang).HasName("PK__Tang__C94DDB5FE01FC899");

            entity.ToTable("Tang");

            entity.Property(e => e.IdTang).HasColumnName("ID_Tang");
            entity.Property(e => e.TenTang).HasMaxLength(250);
        });

        modelBuilder.Entity<ThanhPhan>(entity =>
        {
            entity.HasKey(e => new { e.IdMonAn, e.IdnguyenLieuCs });

            entity.ToTable("ThanhPhan");

            entity.Property(e => e.IdMonAn).HasColumnName("ID_MonAn");
            entity.Property(e => e.IdnguyenLieuCs).HasColumnName("IDNguyenLieuCS");
            entity.Property(e => e.UocGia).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdMonAnNavigation).WithMany(p => p.ThanhPhans)
                .HasForeignKey(d => d.IdMonAn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThanhPhan_MonAn");

            entity.HasOne(d => d.IdnguyenLieuCsNavigation).WithMany(p => p.ThanhPhans)
                .HasForeignKey(d => d.IdnguyenLieuCs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ThanhPhan_NguyenLieuCoSan");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.CodeVoucher).HasName("PK__Voucher__0A25D138FD860863");

            entity.ToTable("Voucher");

            entity.Property(e => e.CodeVoucher).HasColumnName("Code_Voucher");
            entity.Property(e => e.LoaiMa)
                .HasMaxLength(250)
                .HasColumnName("LoaiMA");
            entity.Property(e => e.Mota).HasMaxLength(250);
            entity.Property(e => e.Phantram).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
