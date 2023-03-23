using HRMData.Application.Interfaces;
using HRMData.Domain.Entities.Common;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _db;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(T entity)
        {
            _db.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _db.FindAsync<T>(id);
        }
        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
