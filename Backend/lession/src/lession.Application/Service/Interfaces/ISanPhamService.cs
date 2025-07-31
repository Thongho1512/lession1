using lession.API.DTOs.Common;
using lession.API.DTOs.SanPham;
using lession.Application.DTOs.Common;
using static lession.Application.DTOs.Common.QueryParameters;


namespace lession.Application.Service.Interfaces
{
    public interface ISanPhamService
    {
        Task<ResponseDto<SanPhamDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<SanPhamDto>>> GetAllAsync();
        Task<ResponseDto<SanPhamDto>> CreateAsync(CreateSanPhamDto createDto);
        Task<ResponseDto<SanPhamDto>> UpdateAsync(int id, UpdateSanPhamDto updateDto);
        Task<ResponseDto<bool>> DeleteAsync(int id);
        Task<ResponseDto<bool>> ActiveSanPhamIsSoftDeleted(int id);

        // for pagination
        Task<ResponseDto<PagedResult<SanPhamDto>>> GetPagedAsync(ActiveQueryParameters queryParameters);

    }
}
