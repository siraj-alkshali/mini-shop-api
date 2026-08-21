namespace MiniShop.Application.Shipping;

public class ShippingOptions
{
    public decimal FreeShippingThreshold { get; set; }
    public decimal StandardShippingCost { get; set; }
    public decimal ExpressShippingCost { get; set; }
}