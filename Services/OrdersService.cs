using Constracts.DTO;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using Services.Abtractions;

public class OrdersService : IOrdersService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventRepository _eventRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrdersService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IEventRepository eventRepository, IOrderItemRepository orderItemRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _eventRepository = eventRepository;
        _orderItemRepository = orderItemRepository; 
    }
    // thêm đơn hàng 
    public async Task<int> CreateOrderAsync(OrderDTO orderDTO)
    {
        if (orderDTO == null)
            throw new ArgumentNullException(nameof(orderDTO), "OrderDTO cannot be null.");

        // Kiểm tra sự kiện có hợp lệ không
        var eventEntity = await _eventRepository.GetByIdAsync(orderDTO.EventId);
        if (eventEntity == null)
            throw new Exception("Event không hợp lệ.");

        // Tạo đơn hàng
        var order = new Orders
        {
            FirstName = orderDTO.FirstName,
            LastName = orderDTO.LastName,
            Email = orderDTO.Email,
            OrderStatus = OrderStatus.Pending, // 1: Đang chờ xử lý
            UserId = orderDTO.UserId,
            EventId = orderDTO.EventId,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsDeleted = false
        };
        await _orderRepository.AddAsync(order); // Thêm vào repository
        // Thêm OrderItems cho đơn hàng
        foreach (var orderItemDTO in orderDTO.OrderItems)
        {
            var orderItem = new OrderItems
            {
                OrderId = order.Id,
                Title = orderItemDTO.Title,
                Quantity = orderItemDTO.Quantity,
                UnitPrice = orderItemDTO.UnitPrice,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            };
            await _orderItemRepository.AddAsync(orderItem); // Thêm vào repository
        }
        await _unitOfWork.CompleteAsync(); // Lưu vào database

        return order.Id; // Trả về ID của đơn hàng mới tạo
    }
}