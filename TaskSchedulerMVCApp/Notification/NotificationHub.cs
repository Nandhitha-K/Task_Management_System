using Microsoft.AspNet.SignalR;

namespace TaskSchedulerMVCApp.Notification
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            await hubContext.Clients.All.ReceiveNotification();
        }
    }
}
