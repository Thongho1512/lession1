namespace lession.API.DTOs.SanPham
{
    public class UpdateSanPhamDto
    {
        public string MaSanPham { get; set; } = string.Empty;
        public string TenSanPham { get; set; } = string.Empty;
        public string MoTa { get; set; } = string.Empty;
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public string DonViTinh { get; set; } = string.Empty;
        // Ngày cập nhật sẽ
    }
}
