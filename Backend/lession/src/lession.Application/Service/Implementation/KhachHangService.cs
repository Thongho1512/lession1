using AutoMapper;
using lession.API.DTOs.Common;
using lession.API.DTOs.KhachHang;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.Data.Entities;
using lession.Infrastructure.Repositories.Interfaces;


namespace lession.Application.Service.Implementation
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public KhachHangService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<KhachHangDto>> CreateAsync(CreateKhachHangDto createDto)
        {
            var exists = await _unitOfWork.KhachHangRepository.ExistsAsync(k => k.MaKhachHang == createDto.MaKhachHang);
            if (exists)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Mã khách hàng đã tồn tại!");
            }
            var khachHang = _mapper.Map<KhachHang>(createDto);
            await _unitOfWork.KhachHangRepository.AddAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<KhachHangDto>(khachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto, "Tạo khách hàng thành công!");
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            var khachHang = await _unitOfWork.KhachHangRepository.GetByIdAsync(id);
            if (khachHang == null)
            {
                return ResponseDto<bool>.ErrorResponse("Không tìm thấy khách hàng!");
            }
            khachHang.Active = false; // Đánh dấu là không hoạt động
            await _unitOfWork.KhachHangRepository.UpdateAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<bool>.SuccessResponse(true, "Xóa khách hàng thành công!");
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
            if(KhachHang == null)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Không tìm thấy khách hàng!");
            }
            var dto = _mapper.Map<KhachHangDto>(KhachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto);
        }

        public Task<ResponseDto<KhachHangDto>> GetByMaKhachHangAsync(string maKhachHangId)
        {
            //var khachHang = _unitOfWork.KhachHangRepository.GetByMaKhachHangAsync(maKhachHangId);
            return null; // Chưa implement
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
            if(khachHang == null)
            {
                return ResponseDto<KhachHangDto>.ErrorResponse("Không tìm thấy khách hàng!");
            }
            _mapper.Map(updateDto, khachHang);
            await _unitOfWork.KhachHangRepository.UpdateAsync(khachHang);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<KhachHangDto>(khachHang);
            return ResponseDto<KhachHangDto>.SuccessResponse(dto, "Cập nhật khách hàng thành công!");
        }
    }
}
