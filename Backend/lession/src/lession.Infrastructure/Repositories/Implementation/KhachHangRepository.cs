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
    public class KhachHangRepository : GenericRepository<KhachHang>, IKhachHangRepository
    {
        public KhachHangRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<KhachHang?> GetByMaKhachHang(string maKhachHang)
        {
            return _dbSet.FirstOrDefaultAsync(k => k.MaKhachHang == maKhachHang);
        }

        public async Task<KhachHang?> GetByMaKhachHangAsync(string maKhachHang)
        {
            return await _dbSet.FirstOrDefaultAsync(k => k.MaKhachHang == maKhachHang);
        }

        public async Task<IEnumerable<KhachHang>> SearchByNameAsync(string TenKhachHang)
        {
            return await _dbSet
                .Where(k => k.TenKhachHang.Contains(TenKhachHang))
                .ToListAsync();
        }

        // For pagination
        public IQueryable<KhachHang> GetActiveKhachHangsQuery(bool? active = null)
        {
            var query = _dbSet.AsQueryable();

            if (active.HasValue)
            {
                query = query.Where(k => k.Active == active.Value);
            }

            return query;
        }

        public IQueryable<KhachHang> SearchKhachHangsQuery(string searchTerm)
        {
            return _dbSet.Where(k =>
                k.MaKhachHang.Contains(searchTerm) ||
                k.TenKhachHang.Contains(searchTerm) ||
                (k.Email != null && k.Email.Contains(searchTerm)) ||
                (k.SoDienThoai != null && k.SoDienThoai.Contains(searchTerm))
            );
        }
    }
}
