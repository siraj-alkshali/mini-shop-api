namespace MiniShop.Application.Payments;

public record PaymentRequest(int CustomerId, decimal OrderAmount);