namespace lession.API.DTOs.KhachHang
{
    public class KhachHangDto
    {
        public int Id { get; set; }
        public string TenKhachHang { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NgaySinh { get; set; } = string.Empty ;
        public string DiaChi { get; set; } = string.Empty;
        public bool? Active { get; set; }
        public string NgayTao { get; set; } = string.Empty;
        public string NgayCapNhat { get; set; } = string.Empty;
    }
}
