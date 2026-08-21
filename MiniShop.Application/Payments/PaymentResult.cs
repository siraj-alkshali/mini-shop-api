namespace MiniShop.Application.Payments;

public record PaymentResult(bool IsSuccessful, string? TransactionId);