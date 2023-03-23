using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfTitleRepository : GenericRepository<Title>, ITitleRepository
    {
        private readonly ApplicationDbContext _db;

        public EfTitleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<string> FindNameAsync(string id)
        {
            var TitleId = await _db.Titles.FirstOrDefaultAsync(x => x.Id == id);
            return TitleId!.Name;
        }
        public async Task<string> FindIdAsync(string name)
        {
            var TitleId = await _db.Titles.FirstOrDefaultAsync(x => x.Name == name);
            return TitleId!.Id;
        }
    }
}
