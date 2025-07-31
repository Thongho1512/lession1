using lession.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.Repositories.Interfaces
{
    public interface IKhachHangRepository : IGenericRepository<KhachHang>
    {
        Task<KhachHang?> GetByMaKhachHang(string maKhachHang);
        Task<IEnumerable<KhachHang>> SearchByNameAsync(string tenKhachHang);

        // For pagination
        IQueryable<KhachHang> GetActiveKhachHangsQuery(bool? active = null);
        IQueryable<KhachHang> SearchKhachHangsQuery(string searchTerm);

    }
}
