namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IPaymentGateway
    {

        public Task UpdatePaymentStatusAsync(Guid orderId, string paymentStatus);
    }
}
