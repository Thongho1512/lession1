using System;
using System.Collections.Generic;

namespace lession.Infrastructure.Data.Entities;

public partial class SanPham
{
    public int Id { get; set; }

    public string? MaSanPham { get; set; }

    public string? TenSanPham { get; set; }

    public string? MoTa { get; set; }

    public float GiaBan { get; set; }

    public float SoLuongTon { get; set; }

    public string? DonViTinh { get; set; }

    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}