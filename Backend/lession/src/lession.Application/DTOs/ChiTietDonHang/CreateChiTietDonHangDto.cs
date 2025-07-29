namespace lession.API.DTOs.ChiTietDonHang
{
    public class CreateChiTietDonHangDto
    {
        public int DonHangId { get; set; }
        public int SanPhamId { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        // Optional: You can add validation attributes if needed
        // [Required]
        // [Range(1, int.MaxValue, ErrorMessage = "SoLuong must be greater than 0.")]
        // public int SoLuong { get; set; }
    }
}
