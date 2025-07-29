using System;
using System.Collections.Generic;

namespace lession.Infrastructure.Data.Entities;

public partial class KhachHang
{
    public int Id { get; set; }

    public string? MaKhachHang { get; set; }

    public string? TenKhachHang { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
