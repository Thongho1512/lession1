using lession.Infrastructure.Data;
using lession.Infrastructure.Data.Entities;
using lession.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.Repositories.Implementation
{
    public class ChiTietDonHangRepository : GenericRepository<ChiTietDonHang>, IChiTietDonHangRepository
    {
        public ChiTietDonHangRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ChiTietDonHang>> GetByDonHangIdAsync(int donHangId)
        {
            return await _dbSet
                .Where(ct => ct.DonHangId == donHangId)
                .Include(ct => ct.SanPham)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChiTietDonHang>> GetBySanPhamIdAsync(int sanPhamId)
        {
            return await _dbSet
                .Where(ct => ct.SanPhamId == sanPhamId)
                .Include(ct => ct.DonHang)
                .ToListAsync();
        }
    }
}
