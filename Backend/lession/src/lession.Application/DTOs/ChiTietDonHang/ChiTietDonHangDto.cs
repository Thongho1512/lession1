namespace lession.API.DTOs.ChiTietDonHang
{
    public class ChiTietDonHangDto
    {
        public int Id { get; set; }
        public int DonHangId { get; set; }
        public int SanPhamId { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
