using System;
using System.Threading.Tasks;

namespace Identity.DAL
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
