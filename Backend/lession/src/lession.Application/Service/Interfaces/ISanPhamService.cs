using lession.API.DTOs.Common;
using lession.API.DTOs.SanPham;


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
    }
}
