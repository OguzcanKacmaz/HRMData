using HRMData.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRMData.Persistence.Services
{
    public class RequestCleanupService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private readonly Timer _timer;

        public RequestCleanupService(IServiceProvider services)
        {
            _services = services;
            _timer = new Timer(DoWork!, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // 1 dakika aralıklarla kontrol eder.
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var scope = _services.CreateScope();
            var requestService = scope.ServiceProvider.GetRequiredService<IAdvanceRequestService>();
            await requestService.DeleteExpiredAdvanceRequests();
            await requestService.AnnualLeaveRenewalAsync();
            await requestService.ResetDateAsync();
            scope.Dispose();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
