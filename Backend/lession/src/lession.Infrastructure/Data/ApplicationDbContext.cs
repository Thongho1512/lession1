using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using lession.Infrastructure.Data.Entities;

namespace lession.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietD__3214EC0740537410");

            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.ThanhTien).HasComputedColumnSql("([SoLuong]*[DonGia])", true);

            entity.HasOne(d => d.DonHang).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.DonHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_DonHang");

            entity.HasOne(d => d.SanPham).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_SanPham");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DonHang__3214EC074CF425F7");

            entity.ToTable("DonHang");

            entity.HasIndex(e => e.MaDonHang, "UQ__DonHang__129584AC27A61352").IsUnique();

            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.MaDonHang).HasMaxLength(50);
            entity.Property(e => e.NgayDat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.KhachHang).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.KhachHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonHang_KhachHang");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KhachHan__3214EC070DDF781A");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Email, "UQ__KhachHan__A9D105348CD6BECC").IsUnique();

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.MaKhachHang).HasMaxLength(50);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai).HasMaxLength(20);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SanPham__3214EC0784FA458C");

            entity.ToTable("SanPham");

            entity.HasIndex(e => e.MaSanPham, "UQ__SanPham__FAC7442C1AD2BCF6").IsUnique();

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DonViTinh).HasMaxLength(50);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaSanPham).HasMaxLength(50);
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoLuongTon).HasDefaultValue(0);
            entity.Property(e => e.TenSanPham).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
