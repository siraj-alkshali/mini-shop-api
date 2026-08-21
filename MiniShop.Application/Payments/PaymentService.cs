namespace MiniShop.Application.Payments;

public class PaymentService : IPaymentService
{
    public Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        if (request.OrderAmount <= 0)
            return Task.FromResult(new PaymentResult(false, null));

        return Task.FromResult(new PaymentResult(true, Guid.NewGuid().ToString()));
    }
}