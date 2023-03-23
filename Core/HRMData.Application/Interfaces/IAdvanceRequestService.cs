namespace HRMData.Application.Interfaces
{
    public interface IAdvanceRequestService
    {
        public Task DeleteExpiredAdvanceRequests();
        public Task AnnualLeaveRenewalAsync();
        public Task ResetDateAsync();
    }
}
