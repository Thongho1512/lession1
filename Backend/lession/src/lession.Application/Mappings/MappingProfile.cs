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
            //_mapper.Map<TDestination>(source); create a new instance of TDestination
            // _mapper.Map(source, destination);
            // KhachHang mappings
            CreateMap<KhachHang, KhachHangDto>()
                .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src => src.NgaySinh.HasValue ? src.NgaySinh.Value.ToString("dd-MM-yyyy") : string.Empty))
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => src.NgayTao.HasValue ? src.NgayTao.Value.ToString("dd-MM-yyyy HH:mm:ss") : string.Empty))
                .ForMember(dest => dest.NgayCapNhat, opt => opt.MapFrom(src => src.NgayCapNhat.HasValue ? src.NgayCapNhat.Value.ToString("dd-MM-yyyy HH:mm:ss") : string.Empty));


            CreateMap<CreateKhachHangDto, KhachHang>()
                .ForMember(dest => dest.NgaySinh, opt => opt.Ignore());
            CreateMap<UpdateKhachHangDto, KhachHang>()
                .ForMember(dest => dest.NgaySinh, opt => opt.Ignore())
                .ForMember(dest => dest.NgayCapNhat, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // SanPham mappings
            CreateMap<SanPham, SanPhamDto>()
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => src.NgayTao.HasValue ? src.NgayTao.Value.ToString("dd-MM-yyyy HH:mm:ss") : string.Empty))
                .ForMember(dest => dest.NgayCapNhat, opt => opt.MapFrom(src => src.NgayCapNhat.HasValue ? src.NgayCapNhat.Value.ToString("dd-MM-yyyy HH:mm:ss") : string.Empty));
            CreateMap<CreateSanPhamDto, SanPham>()
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdateSanPhamDto, SanPham>()
                .ForMember(dest => dest.NgayTao, opt => opt.MapFrom(src => DateTime.Now))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // DonHang mappings
            CreateMap<DonHang, DonHangDto>()
                .ForMember(dest => dest.NgayDat, opt => opt.MapFrom(src => src.NgayDat.HasValue ? src.NgayDat.Value.ToString("dd-MM-yyyy") : string.Empty))
                .ForMember(dest => dest.NgayCapNhat, opt => opt.MapFrom(src => src.NgayCapNhat.HasValue ? src.NgayCapNhat.Value.ToString("dd-MM-yyyy HH:mm:ss") : string.Empty));

            //.ForMember(dest => dest.TenKhachHang, opt => opt.MapFrom(src => src.KhachHang != null ? src.KhachHang.TenKhachHang : null));
            CreateMap<CreateDonHangDto, DonHang>()
                //.ForMember(dest => dest.NgayDat, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ChiTietDonHangs, opt => opt.Ignore());
            CreateMap<UpdateDonHangDto, DonHang>()
                .ForMember(dest => dest.ChiTietDonHangs, opt => opt.Ignore());

            // ChiTietDonHang mappings
            CreateMap<ChiTietDonHang, ChiTietDonHangDto>();
            //.ForMember(dest => dest.TenSanPham, opt => opt.MapFrom(src => src.SanPham != null ? src.SanPham.TenSanPham : null));
            CreateMap<CreateChiTietDonHangDto, ChiTietDonHang>();
            CreateMap<UpdateChiTietDonHangDto, ChiTietDonHang>();
        }
    }
}