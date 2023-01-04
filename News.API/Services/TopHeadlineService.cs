using News.API.Services.Interfaces;

namespace News.API.Services
{
    public class TopHeadlineService : BackgroundService, ITopHeadlineService
    {
        private readonly PeriodicTimer _periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _periodicTimer.WaitForNextTickAsync(stoppingToken)
                && !stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync();
            }
        }

        private Task DoWorkAsync()
        {
            throw new NotImplementedException();
        }
    }
}
