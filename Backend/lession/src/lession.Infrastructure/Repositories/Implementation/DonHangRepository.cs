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
    public class DonHangRepository : GenericRepository<DonHang>, IDonHangRepository
    {
        public DonHangRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<DonHang?> GetByMaDonHangAsync(string maDonHang)
        {
            return await _dbSet.FirstOrDefaultAsync(d => d.MaDonHang == maDonHang);
        }

        public async Task<IEnumerable<DonHang>> GetByKhachHangIdAsync(int khachHangId)
        {
            return await _dbSet
                .Where(d => d.KhachHangId == khachHangId)
                .Include(d => d.KhachHang)
                .ToListAsync();
        }

        public async Task<DonHang?> GetWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(d => d.KhachHang)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(d => d.Id == id);
        }


    }
}
