using AutoMapper;
using lession.API.DTOs.Common;
using lession.API.DTOs.KhachHang;
using lession.Application.DTOs.Common;
using lession.Application.Extensions;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.Data.Entities;
using lession.Infrastructure.Repositories.Interfaces;
using System;
using System.Globalization;
using System.Linq.Expressions;
using static lession.Application.DTOs.Common.QueryParameters;


namespace lession.Application.Service.Implementation
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStaticJsonGeneratorService _jsonGenerator;

        // Define ordering mappings
        private readonly Dictionary<string, Expression<Func<KhachHang, object>>> _orderMappings = new()
        {
            { "makhachhang", x => x.MaKhachHang },
            { "tenkhachhang", x => x.TenKhachHang },
            { "email", x => x.Email ?? "" },
            { "ngaytao", x => x.NgayTao }
        };
        public KhachHangService(IUnitOfWork unitOfWork, IMapper mapper, IStaticJsonGeneratorService jsonGenerator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jsonGenerator = jsonGenerator;
        }

        public async Task<ResponseDto<KhachHangDto>> CreateAsync(CreateKhachHangDto createDto)
        {
            var exists = await _unitOfWork.KhachHangRepository.ExistsAsync(k => k.MaKhachHang == createDto.MaKhachHang);
            if (exists)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Mã khách hàng đã tồn tại.");
            }
          
            var khachHang = _mapper.Map<KhachHang>(createDto);
            if(!HandleDateDataType.ParseStringToDateOnly(createDto.NgaySinh, out DateOnly result))
                return ResponseDto<KhachHangDto>.ErrorResponse("Ngày sinh không hợp lệ. Vui lòng nhập đúng định dạng dd-MM-yyyy.");
            khachHang.NgaySinh = result;
            await _unitOfWork.KhachHangRepository.AddAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();

            // Generate JSON file asynchronously if customer is active

            _ = Task.Run(async () => await _jsonGenerator.GenerateKhachHangJsonAsync());

            var dto = _mapper.Map<KhachHangDto>(khachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto, "Tạo khách hàng thành công.");
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            var khachHang = await _unitOfWork.KhachHangRepository.GetByIdAsync(id);
            if (khachHang == null)
            {
                return ResponseDto<bool>.ErrorResponse("Không tìm thấy khách hàng.");
            }
            khachHang.Active = false; // Đánh dấu là không hoạt động
            await _unitOfWork.KhachHangRepository.UpdateAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();

            // Generate JSON 
            _ = Task.Run(async () => await _jsonGenerator.GenerateKhachHangJsonAsync());

            return ResponseDto<bool>.SuccessResponse(true, "Xóa khách hàng thành công.");
        }

        public async Task<ResponseDto<IEnumerable<KhachHangDto>>> GetAllAsync()
        {
            IEnumerable<KhachHang> KhachHangs = await _unitOfWork.KhachHangRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<KhachHangDto>>(KhachHangs);
            return ResponseDto<IEnumerable<KhachHangDto>>.SuccessResponse(dtoList);
        }

        public async Task<ResponseDto<KhachHangDto>> GetByIdAsync(int id)
        {
            var KhachHang = await _unitOfWork.KhachHangRepository.GetByIdAsync(id);
            if (KhachHang == null)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Không tìm thấy khách hàng.");
            }
            var dto = _mapper.Map<KhachHangDto>(KhachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto);
        }

        public Task<ResponseDto<KhachHangDto>> GetByMaKhachHangAsync(string maKhachHangId)
        {
            //var khachHang = _unitOfWork.KhachHangRepository.GetByMaKhachHangAsync(maKhachHangId);
            return null; // Chưa implement
        }

        public async Task<ResponseDto<KhachHangDto>> GetKhachHangIsDeleted(int id)
        {
            var khachHang = await _unitOfWork.KhachHangRepository.GetByIdAsync(id);
            if (khachHang == null)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Không tìm thấy khách hàng đã xóa.");
            }
            if (khachHang.Active == true)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Khách hàng này chưa bị xóa.");
            }
            khachHang.Active = true; // Đánh dấu là không hoạt động
            await _unitOfWork.KhachHangRepository.UpdateAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();

            // Generate JSON
            _ = Task.Run(async () => await _jsonGenerator.GenerateKhachHangJsonAsync());

            var dto = _mapper.Map<KhachHangDto>(khachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto, "Khôi phục khách hàng thành công.");
        }


        public async Task<ResponseDto<IEnumerable<KhachHangDto>>> SearchByNameAsync(string searchTerm)
        {
            IEnumerable<KhachHang> khachHangs = await _unitOfWork.KhachHangRepository.SearchByNameAsync(searchTerm);
            var dtoList = _mapper.Map<IEnumerable<KhachHangDto>>(khachHangs);
            return ResponseDto<IEnumerable<KhachHangDto>>.SuccessResponse(dtoList);
        }

        public async Task<ResponseDto<KhachHangDto>> UpdateAsync(int id, UpdateKhachHangDto updateDto)
        {
            var khachHang = await _unitOfWork.KhachHangRepository.GetByIdAsync(id);
            if (khachHang == null)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Không tìm thấy khách hàng.");
            }
            var oldActiveStatus = khachHang.Active;

            _mapper.Map(updateDto, khachHang);
            if(!HandleDateDataType.ParseStringToDateOnly(updateDto.NgaySinh, out DateOnly result))
                return ResponseDto<KhachHangDto>.ErrorResponse("Ngày sinh không hợp lệ. Vui lòng nhập đúng định dạng dd-MM-yyyy.");
            khachHang.NgaySinh = result;
            await _unitOfWork.KhachHangRepository.UpdateAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();

            // Generate JSON 
            _ = Task.Run(async () => await _jsonGenerator.GenerateKhachHangJsonAsync());

            var dto = _mapper.Map<KhachHangDto>(khachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto, "Cập nhật khách hàng thành công.");
        }

        // For pagination
        public async Task<ResponseDto<PagedResult<KhachHangDto>>> GetPagedAsync(ActiveQueryParameters queryParameters)
        {
            // Start with base query including active filter
            var query = _unitOfWork.KhachHangRepository.GetActiveKhachHangsQuery(queryParameters.Active);

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                query = query.Where(k =>
                    k.MaKhachHang.Contains(queryParameters.SearchTerm) ||
                    k.TenKhachHang.Contains(queryParameters.SearchTerm) ||
                    (k.Email != null && k.Email.Contains(queryParameters.SearchTerm)) ||
                    (k.SoDienThoai != null && k.SoDienThoai.Contains(queryParameters.SearchTerm))
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
            var dtos = _mapper.Map<List<KhachHangDto>>(pagedResult.Items);
            var result = new PagedResult<KhachHangDto>(
                dtos,
                pagedResult.TotalCount,
                pagedResult.PageNumber,
                pagedResult.PageSize);

            return ResponseDto<PagedResult<KhachHangDto>>.SuccessResponse(result, "Lấy danh sách khách hàng thành công.");
        }

    }
}