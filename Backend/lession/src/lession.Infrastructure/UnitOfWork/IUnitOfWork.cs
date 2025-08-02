using lession.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IKhachHangRepository KhachHangRepository { get; }
        ISanPhamRepository SanPhamRepository { get; }
        IDonHangRepository DonHangRepository { get; }
        IChiTietDonHangRepository ChiTietDonHangRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
