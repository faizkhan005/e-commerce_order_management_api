using E_CommerceOrderManagementAPI.Application.Interfaces;

namespace E_CommerceOrderManagementAPI.Infrastructure.Payments
{
    public class FakePaymentGateway : IPaymentGateway
    {
        public Task<bool> ProcessPayment(Guid orderId, decimal lineTotal)
        {
            Console.WriteLine("PaymentCompleted");

            return Task.FromResult(true);
        }
    }
}
