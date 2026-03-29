namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface INotificationService
    {
        public Task SendOrderStatusUpdateAsync(Guid orderId, string status, string message);
    }
}
