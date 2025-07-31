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
    public class SanPhamRepository : GenericRepository<SanPham>, ISanPhamRepository
    {
        public SanPhamRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SanPham?> GetByMaSanPhamAsync(string maSanPham)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.MaSanPham == maSanPham);
        }

        public async Task UpdateStockAsync(int sanPhamId, float v)
        {
            var sanPham = _dbSet.Find(sanPhamId);
            if(sanPham == null)
            {
                throw new KeyNotFoundException($"Sản phẩm với ID {sanPhamId} không tồn tại.");
            }
            sanPham.SoLuongTon = sanPham.SoLuongTon - v;
            if (sanPham.SoLuongTon < 0)
            {
                throw new InvalidOperationException("Số lượng tồn không thể âm.");
            }
            _dbSet.Update(sanPham);
            await _context.SaveChangesAsync();
        }

        // For pagination
        public IQueryable<SanPham> GetActiveSanPhamsQuery(bool? active = null)
        {
            var query = _dbSet.AsQueryable();

            if (active.HasValue)
            {
                query = query.Where(s => s.Active == active.Value);
            }

            return query;
        }

        public IQueryable<SanPham> SearchSanPhamsQuery(string searchTerm)
        {
            return _dbSet.Where(s =>
                s.MaSanPham.Contains(searchTerm) ||
                s.TenSanPham.Contains(searchTerm) ||
                (s.MoTa != null && s.MoTa.Contains(searchTerm))
            );
        }
    }
}
