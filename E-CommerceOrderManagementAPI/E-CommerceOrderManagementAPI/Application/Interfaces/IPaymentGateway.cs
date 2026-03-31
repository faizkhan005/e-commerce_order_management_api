namespace E_CommerceOrderManagementAPI.Application.Interfaces
{
    public interface IPaymentGateway
    {

        public Task<bool> ProcessPayment(Guid orderId, decimal lineTotal);
    }
}
    