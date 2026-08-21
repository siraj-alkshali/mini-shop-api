using Microsoft.AspNetCore.Mvc;
using MiniShop.Application.Common;
using MiniShop.Application.DTOs;
using MiniShop.Application.DTOs.Orders;
using MiniShop.Application.Services;
using MiniShop.Api.Extensions;

namespace MiniShop.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{orderId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDetails>> GetOrderDetails(int orderId)
    {
        OrderDetails? orderDetails = await _orderService.GetOrderDetailsAsync(orderId);

        return orderDetails == null ? NotFound() : Ok(orderDetails);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderDetails), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<OrderDetails>> CreateOrder(CreateOrderRequest request)
    {
        ServiceResult<OrderDetails> result = await _orderService.CreateOrderAsync(request);

        if (!result.IsSuccess)
            return this.ToActionResult(result);

        return CreatedAtAction(nameof(GetOrderDetails), new { orderId = result.Data!.Id }, result.Data);
    }
}