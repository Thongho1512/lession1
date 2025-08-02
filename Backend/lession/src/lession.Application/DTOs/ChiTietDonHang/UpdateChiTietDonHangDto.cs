namespace lession.API.DTOs.ChiTietDonHang
{
    public class UpdateChiTietDonHangDto
    {
        public int Id { get; set; }
        public int DonHangId { get; set; }
        public int SanPhamId { get; set; }
        public float SoLuong { get; set; }
    }
}
