using AutoMapper;
using Azure;
using lession.API.DTOs.Common;
using lession.API.DTOs.SanPham;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.Data.Entities;
using lession.Infrastructure.Repositories.Interfaces;


namespace lession.Application.Service.Implementation
{
    public class SanPhamService : ISanPhamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // constructor
        public SanPhamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<bool>> ActiveSanPhamIsSoftDeleted(int id)
        {
            var sanPham = await _unitOfWork.SanPhamRepository.GetByIdAsync(id);
            if(sanPham == null)
            {
                return ResponseDto<bool>.ErrorResponse("Không tìm thấy sản phẩm.");
            }
            sanPham.Active = true; // Active the soft-deleted product
            await _unitOfWork.SanPhamRepository.UpdateAsync(sanPham);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<bool>.SuccessResponse(true, "Kích hoạt sản phẩm thành công.");
        }

        // methods
        public async Task<ResponseDto<SanPhamDto>> CreateAsync(CreateSanPhamDto createDto)
        {
            var exists = await _unitOfWork.SanPhamRepository.ExistsAsync(k => k.TenSanPham == createDto.TenSanPham);
            if (exists)
            {
                return ResponseDto<SanPhamDto>.ErrorResponse("Sản phẩm đã tồn tại.");
            }
            var sanPham = _mapper.Map<SanPham>(createDto);
            await _unitOfWork.SanPhamRepository.AddAsync(sanPham);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<SanPhamDto>(sanPham);
            return ResponseDto<SanPhamDto>.SuccessResponse(dto, "Tạo sản phẩm thành công.");
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            var sanPham = await _unitOfWork.SanPhamRepository.GetByIdAsync(id);
            if (sanPham == null)
            {
                return ResponseDto<bool>.ErrorResponse("Không tìm thấy sản phẩm.");
            }
            sanPham.Active = false; // Soft delete
            await _unitOfWork.SanPhamRepository.UpdateAsync(sanPham);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<bool>.SuccessResponse(true, "Xóa sản phẩm thành công.");
        }

        public async Task<ResponseDto<IEnumerable<SanPhamDto>>> GetAllAsync()
        {
            var sanphams = await _unitOfWork.SanPhamRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<SanPhamDto>>(sanphams);
            return ResponseDto<IEnumerable<SanPhamDto>>.SuccessResponse(dtos, "Lấy danh sách sản phẩm thành công.");
        }

        public async Task<ResponseDto<SanPhamDto>> GetByIdAsync(int id)
        {
            var sanPham = await _unitOfWork.SanPhamRepository.GetByIdAsync(id);
            if(sanPham == null)
            {
                return ResponseDto<SanPhamDto>.ErrorResponse("Không tìm thấy sản phẩm.");
            }
            // _mapper.Map<TDestination>(source); create a new instance of TDestination
            var sanPhamDto = _mapper.Map<SanPhamDto>(sanPham);
            return ResponseDto<SanPhamDto>.SuccessResponse(sanPhamDto, "Lấy sản phẩm thành công.");
        }


        async Task<ResponseDto<SanPhamDto>> ISanPhamService.UpdateAsync(int id, UpdateSanPhamDto updatedto)
        {
            var sanPham = await _unitOfWork.SanPhamRepository.GetByIdAsync(id);
            if (sanPham == null)
            {
                return ResponseDto<SanPhamDto>.ErrorResponse("Không tìm thấy sản phẩm.");
            }
            // Cập nhật thông tin sản phẩm
            // _mapper.Map(source, destination);
            _mapper.Map(updatedto, sanPham);
            await _unitOfWork.SanPhamRepository.UpdateAsync(sanPham);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<SanPhamDto>.SuccessResponse(_mapper.Map<SanPhamDto>(sanPham), "Cập nhật sản phẩm thành công.");
        }
    }
}
