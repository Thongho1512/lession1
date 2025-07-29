using lession.Infrastructure.Data;
using lession.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Infrastructure.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        private IKhachHangRepository? _khachHangRepository;
        private ISanPhamRepository? _sanPhamRepository;
        private IDonHangRepository? _donHangRepository;
        private IChiTietDonHangRepository? _chiTietDonHangRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IKhachHangRepository KhachHangRepository =>
            _khachHangRepository ??= new KhachHangRepository(_context);

        public ISanPhamRepository SanPhamRepository =>
            _sanPhamRepository ??= new SanPhamRepository(_context);

        public IDonHangRepository DonHangRepository =>
            _donHangRepository ??= new DonHangRepository(_context);

        public IChiTietDonHangRepository ChiTietDonHangRepository =>
            _chiTietDonHangRepository ??= new ChiTietDonHangRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
