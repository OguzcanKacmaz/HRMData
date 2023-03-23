using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfDepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public EfDepartmentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
