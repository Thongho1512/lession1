using AutoMapper;
using lession.API.DTOs.Common;
using lession.API.DTOs.DonHang;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.Data.Entities;
using lession.Infrastructure.Repositories.Implementation;
using lession.Infrastructure.Repositories.Interfaces;


namespace lession.Application.Service.Implementation
{
    public class DonHangService : IDonHangService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public async Task<ResponseDto<DonHangDto>> CreateAsync(CreateDonHangDto createDto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var donHang = _mapper.Map<DonHang>(createDto);
                donHang.TongTien = 0;
                // Initialize TongTien to 0, it will be calculated later


            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return ResponseDto<DonHangDto>.ErrorResponse($"Lỗi khi tạo đơn hàng: {ex.Message}");
            }
            return null;
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            var donHang = await _unitOfWork.DonHangRepository.GetByIdAsync(id);
            if (donHang == null)
            {
                return ResponseDto<bool>.ErrorResponse("Không tìm thấy đơn hàng.");
            }
            await _unitOfWork.DonHangRepository.DeleteAsync(donHang);
            await _unitOfWork.SaveChangesAsync();
            return ResponseDto<bool>.SuccessResponse(true, "Xóa đơn hàng thành công.");
        }

        public async Task<ResponseDto<IEnumerable<DonHangDto>>> GetAllAsync()
        {
            var donHangs = await _unitOfWork.DonHangRepository.GetAllAsync();
            if (donHangs == null || !donHangs.Any())
            {
                return ResponseDto<IEnumerable<DonHangDto>>.ErrorResponse("Không có đơn hàng nào.");
            }
            var donHangDtos = _mapper.Map<IEnumerable<DonHangDto>>(donHangs);
            return ResponseDto<IEnumerable<DonHangDto>>.SuccessResponse(donHangDtos, "Lấy danh sách đơn hàng thành công.");
        }

        public async Task<ResponseDto<DonHangDto>> GetByIdAsync(int id)
        {
            var donHang  = await _unitOfWork.DonHangRepository.GetByIdAsync(id);
            if (donHang == null)
            {
                return ResponseDto<DonHangDto>.ErrorResponse("Không tìm thấy đơn hàng.");
            }
            var dto = _mapper.Map<DonHangDto>(donHang);
            return ResponseDto<DonHangDto>.SuccessResponse(dto, "Lấy đơn hàng thành công.");
        }

        public async Task<ResponseDto<IEnumerable<DonHangDto>>> GetByKhachHangIdAsync(int khachHangId)
        {
            return null; // Implement this method based on your requirements
        }

        public Task<ResponseDto<IEnumerable<DonHangDto>>> GetBySanPhamIdAsync(int sanPhamId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<DonHangDto>> UpdateAsync(int id, UpdateDonHangDto updateDto)
        {
            var donHang = await _unitOfWork.DonHangRepository.GetByIdAsync(id);
            if(donHang == null)
            {
                return ResponseDto<DonHangDto>.ErrorResponse("Không tìm thấy đơn hàng.");
            }
            _mapper.Map(updateDto, donHang);
            await _unitOfWork.DonHangRepository.UpdateAsync(donHang);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<DonHangDto>(donHang);
            return ResponseDto<DonHangDto>.SuccessResponse(dto, "Cập nhật đơn hàng thành công.");
        }
    }
}


//public async Task<ResponseDto<DonHangDto>> CreateAsync(CreateDonHangDto createDto)
//{
//    await _unitOfWork.BeginTransactionAsync();
//    try
//    {
//        // Check if MaDonHang already exists
//        var exists = await _unitOfWork.DonHangRepository.ExistsAsync(d => d.MaDonHang == createDto.MaDonHang);
//        if (exists)
//            return ResponseDto<DonHangDto>.FailureResponse("Mã đơn hàng đã tồn tại");

//        // Check if customer exists
//        var khachHang = await _unitOfWork.KhachHangRepository.GetByIdAsync(createDto.KhachHangId);
//        if (khachHang == null)
//            return ResponseDto<DonHangDto>.FailureResponse("Không tìm thấy khách hàng");

//        var donHang = _mapper.Map<DonHang>(createDto);
//        donHang.TongTien = 0;

//        await _unitOfWork.DonHangRepository.AddAsync(donHang);
//        await _unitOfWork.SaveChangesAsync();

//        // Add order details
//        decimal tongTien = 0;
//        foreach (var ctDto in createDto.ChiTietDonHangs)
//        {
//            var sanPham = await _unitOfWork.SanPhamRepository.GetByIdAsync(ctDto.SanPhamId);
//            if (sanPham == null)
//            {
//                await _unitOfWork.RollbackTransactionAsync();
//                return ResponseDto<DonHangDto>.FailureResponse($"Không tìm thấy sản phẩm với ID: {ctDto.SanPhamId}");
//            }

//            if (sanPham.SoLuongTon < ctDto.SoLuong)
//            {
//                await _unitOfWork.RollbackTransactionAsync();
//                return ResponseDto<DonHangDto>.FailureResponse($"Sản phẩm {sanPham.TenSanPham} không đủ số lượng tồn");
//            }

//            var chiTiet = new ChiTietDonHang
//            {
//                DonHangId = donHang.Id,
//                SanPhamId = ctDto.SanPhamId,
//                SoLuong = ctDto.SoLuong,
//                DonGia = sanPham.DonGia,
//                ThanhTien = sanPham.DonGia * ctDto.SoLuong
//            };

//            await _unitOfWork.ChiTietDonHangRepository.AddAsync(chiTiet);

//            // Update product stock
//            await _unitOfWork.SanPhamRepository.UpdateStockAsync(ctDto.SanPhamId, -ctDto.SoLuong);

//            tongTien += chiTiet.ThanhTien;
//        }

//        // Update total amount
//        donHang.TongTien = tongTien;
//        await _unitOfWork.DonHangRepository.UpdateAsync(donHang);
//        await _unitOfWork.SaveChangesAsync();

//        await _unitOfWork.CommitTransactionAsync();

//        var result = await _unitOfWork.DonHangRepository.GetWithDetailsAsync(donHang.Id);
//        var dto = _mapper.Map<DonHangDto>(result);
//        return ResponseDto<DonHangDto>.SuccessResponse(dto, "Tạo đơn hàng thành công");
//    }
//    catch (Exception)
//    {
//        await _unitOfWork.RollbackTransactionAsync();
//        throw;
//    }
//}