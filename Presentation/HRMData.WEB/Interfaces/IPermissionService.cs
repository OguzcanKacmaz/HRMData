namespace HRMData.WEB.Interfaces
{
    public interface IPermissionService
    {
        public Task<bool> PermissionDateCheck(DateTime startDate, DateTime endDate);
    }
}
