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
    }
}
