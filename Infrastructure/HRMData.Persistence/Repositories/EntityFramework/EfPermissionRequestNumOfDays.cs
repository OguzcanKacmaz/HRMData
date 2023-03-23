using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfPermissionRequestNumOfDays : IPermissionRequestNumOfDays
    {
        private readonly ApplicationDbContext _db;

        public EfPermissionRequestNumOfDays(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<PermissionRequestNumOfDays> PermissionRequestNumOfDaysAsync()
        {
            var permissionNumOfDays = await _db.PermissionRequestNumOfDays.FirstOrDefaultAsync();
            return permissionNumOfDays!;
        }
    }
}
