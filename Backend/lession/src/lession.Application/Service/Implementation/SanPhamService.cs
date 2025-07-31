using AutoMapper;
using Azure;
using lession.API.DTOs.Common;
using lession.API.DTOs.SanPham;
using lession.Application.DTOs.Common;
using lession.Application.Extensions;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.Data.Entities;
using lession.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;
using static lession.Application.DTOs.Common.QueryParameters;


namespace lession.Application.Service.Implementation
{
    public class SanPhamService : ISanPhamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Define ordering mappings
        private readonly Dictionary<string, Expression<Func<SanPham, object>>> _orderMappings = new()
        {
            { "masanpham", x => x.MaSanPham },
            { "tensanpham", x => x.TenSanPham }
        };

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

        // For pagination
        public async Task<ResponseDto<PagedResult<SanPhamDto>>> GetPagedAsync(ActiveQueryParameters queryParameters)
        {
            // Start with base query including active filter
            var query = _unitOfWork.SanPhamRepository.GetActiveSanPhamsQuery(queryParameters.Active);

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                query = query.Where(s =>
                    s.MaSanPham.Contains(queryParameters.SearchTerm) ||
                    s.TenSanPham.Contains(queryParameters.SearchTerm) ||
                    (s.MoTa != null && s.MoTa.Contains(queryParameters.SearchTerm))
                );
            }

            // Apply ordering
            query = query.ApplyOrdering(
                queryParameters.OrderBy,
                queryParameters.IsDescending,
                _orderMappings);

            // Apply pagination
            var pagedResult = await query.ToPagedResultAsync(
                queryParameters.PageNumber,
                queryParameters.PageSize);

            // Map to DTOs
            var dtos = _mapper.Map<List<SanPhamDto>>(pagedResult.Items);
            var result = new PagedResult<SanPhamDto>(
                dtos,
                pagedResult.TotalCount,
                pagedResult.PageNumber,
                pagedResult.PageSize);

            return ResponseDto<PagedResult<SanPhamDto>>.SuccessResponse(result, "Lấy danh sách sản phẩm thành công.");
        }

    }
}
