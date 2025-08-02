using lession.API.DTOs.Common;
using lession.API.DTOs.DonHang;
using lession.Application.DTOs.Common;

namespace lession.Application.Service.Interfaces
{
    public interface IDonHangService
    {
        Task<ResponseDto<DonHangDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<DonHangDto>>> GetAllAsync();
        Task<ResponseDto<DonHangDto>> CreateAsync(CreateDonHangDto createDto);
        Task<ResponseDto<DonHangDto>> UpdateAsync(int id, UpdateDonHangDto updateDto);
        Task<ResponseDto<bool>> DeleteAsync(int id);
        Task<ResponseDto<IEnumerable<DonHangDto>>> GetByKhachHangIdAsync(int khachHangId);
        Task<ResponseDto<IEnumerable<DonHangDto>>> GetBySanPhamIdAsync(int sanPhamId);

        Task<ResponseDto<DonHangDto>> GetWithDetails(int id);
        // For pagination
        Task<ResponseDto<PagedResult<DonHangDto>>> GetPagedAsync(QueryParameters queryParameters);

    }
}
