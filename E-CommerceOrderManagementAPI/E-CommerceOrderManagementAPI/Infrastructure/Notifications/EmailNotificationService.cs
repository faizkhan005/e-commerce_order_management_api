using E_CommerceOrderManagementAPI.Application.Interfaces;

namespace E_CommerceOrderManagementAPI.Infrastructure.Notifications
{
    public class EmailNotificationService : INotificationService
    {
        public Task GenerateOrderNotification(Guid orderId, Guid customerID, string status, string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
