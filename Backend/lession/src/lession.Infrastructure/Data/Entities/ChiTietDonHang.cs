using System;
using System.Collections.Generic;

namespace lession.Infrastructure.Data.Entities;

public partial class ChiTietDonHang
{
    public int Id { get; set; }

    public int DonHangId { get; set; }

    public int SanPhamId { get; set; }

    public float SoLuong { get; set; }

    public float ThanhTien { get; set; }

    public virtual DonHang DonHang { get; set; } = null!;

    public virtual SanPham SanPham { get; set; } = null!;
}