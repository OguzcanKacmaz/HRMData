using HRMData.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRMData.Persistence.Services
{
    public class CompanyStatuCheckService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _services;
        private readonly Timer _timer;

        public CompanyStatuCheckService(IServiceProvider services)
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
            var requestService = scope.ServiceProvider.GetRequiredService<ICompanyStatuCheckService>();
            await requestService.CompanyStatuCheck();
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
