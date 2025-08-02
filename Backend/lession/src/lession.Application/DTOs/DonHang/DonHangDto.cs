using lession.API.DTOs.ChiTietDonHang;

namespace lession.API.DTOs.DonHang
{
    public class DonHangDto
    {
        public int Id { get; set; }

        public string MaDonHang { get; set; } = null!;

        public int KhachHangId { get; set; }

        public string NgayDat { get; set; } = string.Empty;
        public string NgayCapNhat { get; set; } = string.Empty;

        public float TongTien { get; set; }

        public string? GhiChu { get; set; }
        public List<ChiTietDonHangDto> ChiTietDonHangs { get; set; } = new List<ChiTietDonHangDto>();
    }
}
