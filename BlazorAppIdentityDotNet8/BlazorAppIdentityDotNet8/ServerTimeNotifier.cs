
using BlazorAppIdentityDotNet8.Components.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BlazorAppIdentityDotNet8
{
    public class ServerTimeNotifier : BackgroundService
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeNotifier> _logger;
        private readonly IHubContext<ChatHub, IChatHubClient> _context;

        public ServerTimeNotifier(IHubContext<ChatHub, IChatHubClient> context, ILogger<ServerTimeNotifier> logger)
        {
            _context = context;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);
            while(!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var datetime = DateTime.Now;
                _logger.LogInformation("Executing {Service} {Time}", nameof(ServerTimeNotifier), datetime);

                _context.Clients.All.ReceiveNotification($"Server time = {datetime}");
            }
        }
    }   
}
