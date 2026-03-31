namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface INotificationService
    {
        public Task GenerateOrderNotification(Guid orderId,Guid customerID, string status, string message);
    }
}
