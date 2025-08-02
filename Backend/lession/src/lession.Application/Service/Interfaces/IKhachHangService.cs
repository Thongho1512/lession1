using lession.API.DTOs.Common;
using lession.API.DTOs.KhachHang;
using lession.Application.DTOs.Common;
using static lession.Application.DTOs.Common.QueryParameters;

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
        Task<ResponseDto<KhachHangDto>> GetKhachHangIsDeleted(int id);

        // for pagination
        Task<ResponseDto<PagedResult<KhachHangDto>>> GetPagedAsync(ActiveQueryParameters queryParameters);
    }
}
