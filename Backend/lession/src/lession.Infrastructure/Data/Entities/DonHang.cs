using System;
using System.Collections.Generic;

namespace lession.Infrastructure.Data.Entities;

public partial class DonHang
{
    public int Id { get; set; }

    public string? MaDonHang { get; set; }

    public int KhachHangId { get; set; }

    public DateOnly? NgayDat { get; set; }
    public DateTime? NgayCapNhat { get; set; }

    public float TongTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhachHang KhachHang { get; set; } = null!;
}