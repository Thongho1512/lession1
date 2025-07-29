using lession.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.Repositories.Interfaces
{
    public interface IDonHangRepository : IGenericRepository<DonHang>
    {
        Task<DonHang?> GetByMaDonHangAsync(string maDonHang);
        Task<IEnumerable<DonHang>> GetByKhachHangIdAsync(int khachHangId);
        Task<DonHang?> GetWithDetailsAsync(int id);
        //Task<IEnumerable<DonHang>> GetByDataRangeAsync(DateOnly startDate, DateOnly endDate);
    }
}
