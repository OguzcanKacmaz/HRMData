using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface IPermissionRequestNumOfDays
    {
        public Task<PermissionRequestNumOfDays> PermissionRequestNumOfDaysAsync();
    }
}
