using System;
using System.Collections.Generic;

namespace lession.API.Data.Entities;

public partial class ChiTietDonHang
{
    public int Id { get; set; }

    public int DonHangId { get; set; }

    public int SanPhamId { get; set; }

    public double? SoLuong { get; set; }

    public double? DonGia { get; set; }

    public double? ThanhTien { get; set; }

    public virtual DonHang DonHang { get; set; } = null!;

    public virtual SanPham SanPham { get; set; } = null!;
}
