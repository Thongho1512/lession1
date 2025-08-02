namespace lession.API.DTOs.SanPham
{
    public class SanPhamDto
    {
        public int Id { get; set; }
        public string MaSanPham { get; set; } = string.Empty;
        public string TenSanPham { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public float GiaBan { get; set; }
        public float SoLuongTon { get; set; }
        public bool? Active { get; set; }
        public string NgayTao { get; set; } = string.Empty;
        public string NgayCapNhat { get; set; } = string.Empty;
    }
}
