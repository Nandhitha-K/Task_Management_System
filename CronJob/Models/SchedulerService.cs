using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskSchedulerMVCApp.Notification;

namespace SchedulerService.Models
{
    public class SchedulerService : IHostedService, IDisposable
    {
        private int executionCount = 0;

        private System.Threading.Timer _timerNotification;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly IHubContext<NotificationHub> _hubContext;

        public SchedulerService(IServiceScopeFactory serviceScopeFactory,
                                Microsoft.AspNetCore.Hosting.IHostingEnvironment env,
                                IConfiguration iconfiguration,
                                IHubContext<NotificationHub> hubContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _env = env;
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timerNotification = new Timer(RunJob, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void RunJob(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                try
                {
                    // Access any service or dependency using scope.ServiceProvider.GetService<YourService>()
                    // Example: var notificationService = scope.ServiceProvider.GetService<INotificationService>();
                    // Check if it's time to send a notification
                    // Example: if (notificationService.IsTimeToSendNotification())
                    //             SendNotification("Notification message");
                    // This is a placeholder for your logic. Replace it with your actual notification logic.
                    //SendNotification("Task is about to start!");
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    hubContext.Clients.All.ReceiveNotification();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                Interlocked.Increment(ref executionCount);
            }
        }

        //private async void SendNotification(string message)
        //{
        //    await _hubContext.;
        //}

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timerNotification?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timerNotification?.Dispose();
        }
    }
}
