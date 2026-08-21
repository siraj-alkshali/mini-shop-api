using AutoMapper;
using MiniShop.Application.Common;
using MiniShop.Application.Discounts;
using MiniShop.Application.DTOs;
using MiniShop.Application.DTOs.Orders;
using MiniShop.Application.Interfaces;
using MiniShop.Application.Payments;
using MiniShop.Application.Shipping;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Enums;

namespace MiniShop.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly CustomerService _customerService;
    private readonly DiscountResolver _discountResolver;
    private readonly ShippingResolver _shippingResolver;
    private readonly IPaymentService _paymentService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper, CustomerService customerService, DiscountResolver discountResolver, ShippingResolver shippingResolver, IPaymentService paymentService, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerService = customerService;
        _discountResolver = discountResolver;
        _shippingResolver = shippingResolver;
        _paymentService = paymentService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private static bool IsItemInStock(Product product, CreateOrderItemRequest item)
    {
        return product.StockQuantity > item.Quantity;
    }

    private async Task<bool> ValidateCustomerAsync(int customerId)
    {
        return await _customerService.ExistsAsync(customerId);
    }

    private static ServiceResult ValidateOrderItems(IReadOnlyDictionary<int, Product> productDict, IEnumerable<CreateOrderItemRequest> items)
    {
        foreach (CreateOrderItemRequest item in items)
        {
            if (!productDict.TryGetValue(item.ProductId, out var product))
                return ServiceResult.Failure([$"Product {item.ProductId} was not found"], FailureType.NotFound);

            if (!IsItemInStock(product, item))
                return ServiceResult.Failure([$"Not enough stock for {product.Name}"], FailureType.Conflict);
        }

        return ServiceResult.Success();
    }

    private static Order BuildOrderEntity(IReadOnlyDictionary<int, Product> productDict, CreateOrderRequest request)
    {
        Order order = new Order
        {
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            ShippingCost = 0,
            Discount = 0
        };

        foreach (CreateOrderItemRequest item in request.Items)
        {
            Product product = productDict[item.ProductId];

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

        return order;
    }

    private static void ReduceStockQuantity(IReadOnlyDictionary<int, Product> productDict, IEnumerable<CreateOrderItemRequest> items)
    {
        foreach (CreateOrderItemRequest item in items)
        {
            Product product = productDict[item.ProductId];
            product.StockQuantity -= item.Quantity;
        }
    }

    public async Task<OrderDetails?> GetOrderDetailsAsync(int orderId)
    {
        return await _orderRepository.GetOrderDetailsAsync(orderId);
    }

    public async Task<ServiceResult<OrderDetails>> CreateOrderAsync(CreateOrderRequest request)
    {
        if (!await ValidateCustomerAsync(request.CustomerId))
            return ServiceResult<OrderDetails>.Failure(["This customer does not exist"], FailureType.NotFound);

        IEnumerable<int> productIds = request.Items.Select(i => i.ProductId);

        List<Product> products = await _productRepository.GetByIdsAsync(productIds);

        IReadOnlyDictionary<int, Product> productsById = products.ToDictionary(p => p.Id);

        ServiceResult orderItemsValidation = ValidateOrderItems(productsById, request.Items);

        if (!orderItemsValidation.IsSuccess)
            return ServiceResult<OrderDetails>.Failure(orderItemsValidation.Errors, orderItemsValidation.ResultType!.Value);

        Order order = BuildOrderEntity(productsById, request);

        decimal subtotal = order.Items.Sum(item => item.UnitPrice * item.Quantity);

        DiscountContext discountContext = new DiscountContext(request.CustomerId, subtotal, request.CouponCode);

        ServiceResult<DiscountResult?> discountResult = await _discountResolver.ResolveAsync(discountContext);

        if (!discountResult.IsSuccess)
            return ServiceResult<OrderDetails>.Failure(discountResult.Errors, discountResult.ResultType!.Value);

        if (discountResult.Data != null)
        {
            order.Discount = discountResult.Data.Amount;
            order.DiscountType = discountResult.Data.Type;
        }

        if (discountResult.Data?.CouponId is int couponId)
            order.CouponUsage = new CouponUsage
            {
                CouponId = couponId,
                CustomerId = request.CustomerId,
                UsedAt = DateTime.UtcNow
            };

        ShippingContext shippingContext = new ShippingContext(request.ShippingMethod, subtotal);

        ServiceResult<decimal> shippingCostCalculationResult = _shippingResolver.ResolveShippingCost(shippingContext);

        if (!shippingCostCalculationResult.IsSuccess)
            return ServiceResult<OrderDetails>.Failure(shippingCostCalculationResult.Errors, shippingCostCalculationResult.ResultType!.Value);

        decimal shippingCost = shippingCostCalculationResult.Data;

        order.ShippingCost = shippingCost;

        decimal finalAmount = subtotal + order.ShippingCost - order.Discount;

        PaymentRequest paymentRequest = new PaymentRequest(request.CustomerId, finalAmount);

        PaymentResult paymentResult = await _paymentService.ProcessPaymentAsync(paymentRequest);

        if (!paymentResult.IsSuccessful)
            return ServiceResult<OrderDetails>.Failure(["Payment failed"], FailureType.Conflict);

        ReduceStockQuantity(productsById, request.Items);

        order.Status = OrderStatus.Confirmed;

        await _orderRepository.AddAsync(order);

        await _unitOfWork.SaveChangesAsync();

        OrderDetails? orderDetails = await GetOrderDetailsAsync(order.Id);

        if (orderDetails is null)
            return ServiceResult<OrderDetails>.Failure(["Order was not found"], FailureType.NotFound);

        return ServiceResult<OrderDetails>.Success(orderDetails);
    }

    // private async ServiceResult<Order> ValidateOrderAsync(int orderId)
    // {
    //     Order? order = await _orderRepository.GetByIdAsync(orderId);

    //     if (order is null)
    //         return ServiceResult<Order>.Failure(["This order does not exist"], FailureType.NotFound);

    //     if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered || order.Status == OrderStatus.Cancelled)
    //         return ServiceResult<Order>.Failure([$"This order is already {order.Status.ToString()}"], FailureType.Conflict);

    //     return ServiceResult<Order>.Success(order);
    // }

    // public async ServiceResult<OrderDetails> CancelOrderAsync(int orderId)
    // {


    // }
}