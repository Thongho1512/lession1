using lession.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.Repositories.Interfaces
{
    public interface IChiTietDonHangRepository : IGenericRepository<ChiTietDonHang>
    {
        Task<IEnumerable<ChiTietDonHang>> GetByDonHangIdAsync(int donHangId);
        Task<IEnumerable<ChiTietDonHang>> GetBySanPhamIdAsync(int sanPhamId);
    }
}
