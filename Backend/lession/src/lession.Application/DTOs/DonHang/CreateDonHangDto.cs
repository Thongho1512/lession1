using lession.API.DTOs.ChiTietDonHang;

namespace lession.API.DTOs.DonHang
{
    public class CreateDonHangDto
    {
        public string MaDonHang { get; set; } = null!;

        public int KhachHangId { get; set; }

        public DateOnly? NgayDat { get; set; }

        public decimal TongTien { get; set; }

        public string? GhiChu { get; set; }

        public List<CreateChiTietDonHangDto> ChiTietDonHangs { get; set; } = new List<CreateChiTietDonHangDto>();
    }
}
