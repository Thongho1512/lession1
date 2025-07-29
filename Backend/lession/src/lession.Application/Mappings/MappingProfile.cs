using AutoMapper;
using lession.API.DTOs.ChiTietDonHang;
using lession.API.DTOs.DonHang;
using lession.API.DTOs.KhachHang;
using lession.API.DTOs.SanPham;
using lession.Infrastructure.Data.Entities;

namespace lession.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // KhachHang mappings
            CreateMap<KhachHang, KhachHangDto>();
            CreateMap<CreateKhachHangDto, KhachHang>()
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdateKhachHangDto, KhachHang>()
                //.ForMember(dest => dest.CapNhat, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // SanPham mappings
            CreateMap<SanPham, SanPhamDto>();
            CreateMap<CreateSanPhamDto, SanPham>()
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdateSanPhamDto, SanPham>()
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // DonHang mappings
            CreateMap<DonHang, DonHangDto>();
            //.ForMember(dest => dest.TenKhachHang, opt => opt.MapFrom(src => src.KhachHang != null ? src.KhachHang.TenKhachHang : null));
            CreateMap<CreateDonHangDto, DonHang>()
                .ForMember(dest => dest.NgayDat, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ChiTietDonHangs, opt => opt.Ignore());

            // ChiTietDonHang mappings
            CreateMap<ChiTietDonHang, ChiTietDonHangDto>();
            //.ForMember(dest => dest.TenSanPham, opt => opt.MapFrom(src => src.SanPham != null ? src.SanPham.TenSanPham : null));
            CreateMap<CreateChiTietDonHangDto, ChiTietDonHang>();
        }
    }
}