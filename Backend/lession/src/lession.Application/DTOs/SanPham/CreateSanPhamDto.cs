namespace lession.API.DTOs.SanPham
{
    public class CreateSanPhamDto
    {
        public string MaSanPham { get; set; } = string.Empty;
        public string TenSanPham { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public bool? Active { get; set; } = true; // Mặc định là true khi tạo mới
        // Ngày tạo và ngày
    }
}
