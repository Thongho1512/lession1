using lession.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.Repositories.Interfaces
{
    public interface ISanPhamRepository : IGenericRepository<SanPham>
    {
        Task<SanPham?> GetByMaSanPhamAsync(string maSanPham);
        Task UpdateStockAsync(int sanPhamId, float v);

        // For pagination
        IQueryable<SanPham> GetActiveSanPhamsQuery(bool? active = null);
        IQueryable<SanPham> SearchSanPhamsQuery(string searchTerm);
    }
}
