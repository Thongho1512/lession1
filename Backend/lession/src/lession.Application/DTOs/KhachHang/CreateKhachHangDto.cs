namespace lession.API.DTOs.KhachHang
{
    public class CreateKhachHangDto
    {
        public string MaKhachHang { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; } = string.Empty;
        // Ngày tạo và ngày cập nhật sẽ được tự động gán giá trị trong quá trình xử lý
        // nên không cần thiết phải có trong DTO khi tạo mới
    }
}
