using E_CommerceOrderManagementAPI.Application.Interfaces;
using E_CommerceOrderManagementAPI.Contracts.Requests;
using E_CommerceOrderManagementAPI.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceOrderManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    #region Create Order
    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest orderDto)
    {
        OrderResponse response = await _orderService.CreateOrderAsync(orderDto);
        return CreatedAtAction(nameof(GetOrderById), new { orderId = response.OrderID }, response);
    }
    #endregion Create Order

    #region Get Order By Id
    [HttpGet("GetOrderById/{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        OrderResponse response = await _orderService.GetOrderByIdAsync(orderId);
        return Ok(response);
    }
    #endregion Get Order By Id

    #region Add Order Item
    [HttpPost("AddOrderItem/{orderId}")]
    public async Task<IActionResult> AddOrderItem(Guid orderId, [FromBody]AddOrderItemRequest orderItemDto)
    {
        await _orderService.AddOrderItem(orderId, orderItemDto);
        return Ok();
    }
    #endregion Add Order Item

    #region Cancel Order 
    [HttpPatch("CancelOrder/{orderId}")]
    public async Task<IActionResult> CancelOrder(Guid orderId)
    {
        await _orderService.CancelOrderAsync(orderId);
        return Ok();
    }
    #endregion Cancel Order


}
