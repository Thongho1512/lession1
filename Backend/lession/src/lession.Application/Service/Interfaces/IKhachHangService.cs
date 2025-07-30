using lession.API.DTOs.Common;
using lession.API.DTOs.KhachHang;

namespace lession.Application.Service.Interfaces
{
    public interface IKhachHangService
    {
        Task<ResponseDto<KhachHangDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<KhachHangDto>>> GetAllAsync();
        Task<ResponseDto<KhachHangDto>> GetByMaKhachHangAsync(string  maKhachHangId);
        Task<ResponseDto<IEnumerable<KhachHangDto>>> SearchByNameAsync(string searchTerm);
        Task<ResponseDto<KhachHangDto>> CreateAsync(CreateKhachHangDto createDto);
        Task<ResponseDto<KhachHangDto>> UpdateAsync(int id, UpdateKhachHangDto updateDto);
        Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
