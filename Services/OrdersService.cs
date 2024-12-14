using Constracts.DTO;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using EmailService;
using Mapster;
using MapsterMapper;
using Services.Abtractions;

public class OrdersService : IOrdersService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAttendeeRepository _attendeeRepository;

    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public OrdersService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = unitOfWork.OrderRepository;
        _eventRepository = unitOfWork.EventRepository;
        _orderItemRepository = unitOfWork.OrderItemRepository; 
        _attendeeRepository = unitOfWork.AttendeeRepository;
        _mapper = mapper;
    }

    //lấy danh sách đơn hàng
    public async Task<List<OrderDTO>> GetOrdersAsync()
    {
        // Lấy danh sách tất cả đơn hàng từ repository
        var orders = await _orderRepository.GetAllAsync();

        // Chuyển đổi danh sách đơn hàng sang DTO
        var orderDTOs = orders.Select(order => new OrderDTO
        {
            Id = order.Id, 
            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
            UserId = order.UserId,
            EventId = order.EventId,
            CreatedDate = order.CreatedDate, 
            ModifiedDate = order.ModifiedDate, 
            OrderItems = order.OrderItems.Select(item => new OrderItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                CreatedDate = item.CreatedDate, 
                ModifiedDate = item.ModifiedDate 
            }).ToList()
        }).ToList();

        return orderDTOs;
    }
    // lấy danh sách đơn hàng theo id event
    public async Task<List<OrderDTO>> GetOrdersByEventIdAsync(int eventId)
    {
        // Lấy danh sách đơn hàng có EventId từ repository
        var orders = await _orderRepository.GetOrdersByEventIdAsync(eventId);

        // Chuyển đổi danh sách đơn hàng sang DTO
        var orderDTOs = orders.Select(order => new OrderDTO
        {
            Id = order.Id, // Thuộc tính từ BaseDTO
            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
            UserId = order.UserId,
            EventId = order.EventId,
            CreatedDate = order.CreatedDate, // Thuộc tính từ BaseDTO
            ModifiedDate = order.ModifiedDate, // Thuộc tính từ BaseDTO
            OrderItems = order.OrderItems.Select(item => new OrderItemDTO
            {
                Id = item.Id, // Thuộc tính từ BaseDTO
                Title = item.Title,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                CreatedDate = item.CreatedDate, // Thuộc tính từ BaseDTO
                ModifiedDate = item.ModifiedDate // Thuộc tính từ BaseDTO
            }).ToList()
        }).ToList();

        return orderDTOs; // trả về danh sách đơn hàng theo id sự kiện
    }
    // lấy danh sách đơn hàng theo id user
    public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId)
    {
        // Lấy danh sách đơn hàng có EventId từ repository
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

        // Chuyển đổi danh sách đơn hàng sang DTO
        var orderDTOs = orders.Select(order => new OrderDTO
        {
            Id = order.Id, // Thuộc tính từ BaseDTO
            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
            UserId = order.UserId,
            EventId = order.EventId,
            CreatedDate = order.CreatedDate, // Thuộc tính từ BaseDTO
            ModifiedDate = order.ModifiedDate, // Thuộc tính từ BaseDTO
            OrderItems = order.OrderItems.Select(item => new OrderItemDTO
            {
                Id = item.Id, // Thuộc tính từ BaseDTO
                Title = item.Title,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                CreatedDate = item.CreatedDate, // Thuộc tính từ BaseDTO
                ModifiedDate = item.ModifiedDate // Thuộc tính từ BaseDTO
            }).ToList()
        }).ToList();

        return orderDTOs; // trả về danh sách đơn hàng theo id user
    }

    // thêm đơn hàng 
    public async Task<int> CreateOrderAsync(OrderDTO orderDTO)
    {
        if (orderDTO == null)
            throw new ArgumentNullException(nameof(orderDTO), "OrderDTO không được rỗng!!");

        // Kiểm tra sự kiện có hợp lệ không
        var eventEntity = await _eventRepository.GetByIdAsync(orderDTO.EventId);
        if (eventEntity == null)
            throw new Exception("Event không hợp lệ.");

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
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
            await _unitOfWork.CompleteAsync(); // Lưu vào database

            // Thêm OrderItems cho đơn hàng
            foreach (var orderItemDTO in orderDTO.OrderItems)
            {
                var orderItem = new OrderItems
                {
                    OrderId = order.Id,
                    TicketId = orderItemDTO.TicketId,
                    Title = orderItemDTO.Title,
                    Quantity = orderItemDTO.Quantity,
                    UnitPrice = orderItemDTO.UnitPrice,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };
                await _orderItemRepository.AddAsync(orderItem); // Thêm vào repository
                await _unitOfWork.TicketRepository.UpdateTicketQuantitySold(orderItemDTO.TicketId, orderItemDTO.Quantity);
            }
            await _unitOfWork.CompleteAsync(); // Lưu vào database
            await transaction.CommitAsync();

            return order.Id; // Trả về ID của đơn hàng mới tạo

        } catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    public async Task<bool> ConfirmOrderAsync(int orderId)
    {
        Console.WriteLine("ConfirmOrder");
        // Tìm đơn hàng theo ID
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new Exception("Không tìm thấy đơn hàng.");

        if (order.OrderStatus != OrderStatus.Pending)
            throw new Exception("Đơn hàng không thể xác nhận vì không ở trạng thái chờ xử lý.");

        // Cập nhật trạng thái đơn hàng
        order.OrderStatus = OrderStatus.Confirmed;
        order.ModifiedDate = DateTime.Now;

        Console.WriteLine(order.OrderStatus);
        await _orderRepository.UpdateAsync(order);

        // Lưu thay đổi
        await _unitOfWork.CompleteAsync();

        // Tạo các bản ghi attendee
        foreach (var orderItem in order.OrderItems)
        {
            for (int i = 0; i < orderItem.Quantity; i++)
            {
                var attendeeDto = new AttendeeDTO
                {
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Email = order.Email,
                    IsCancelled = false,
                    HasArrived = false,
                    ArrivalTime = null,
                    UserId = order.UserId,
                    OrderId = order.Id,
                    TicketId = orderItem.TicketId, 
                    EventId = order.EventId
                };
                var attendee = attendeeDto.Adapt<Attendees>();
                await _attendeeRepository.AddAsync(attendee);
               
            }
        }

        // Lưu các bản ghi attendee
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
    {
        Console.WriteLine("GET ORDER BY ID");
        // Lấy đơn hàng theo ID từ repository
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order == null)
        {
            throw new Exception("Không tìm thấy đơn hàng.");
        }

        // Chuyển đổi đơn hàng sang DTO
        var orderDTO = new OrderDTO
        {
            Id = order.Id,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
            UserId = order.UserId,
            EventId = order.EventId,
            OrderStatus = order.OrderStatus,
            CreatedDate = order.CreatedDate,
            ModifiedDate = order.ModifiedDate,
            OrderItems = order.OrderItems.Select(item => new OrderItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                CreatedDate = item.CreatedDate,
                ModifiedDate = item.ModifiedDate
            }).ToList()
        };

        return orderDTO;
    }

    public async Task<bool> UpdateOrderAsync(OrderDTO order)
    {
        var orderE = await _orderRepository.GetByIdAsync(order.Id);
        if (orderE == null) return false;
        orderE.FirstName = order.FirstName;
        orderE.LastName = order.LastName;
        orderE.Email = order.Email;
        await _unitOfWork.OrderRepository.UpdateAsync(orderE);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<int> CancelledOrderAsync(int orderId)
    {
        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Retrieve the order
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException($"Order with ID {orderId} not found.");
            }

            // Check if the order is cancellable
            if (order.OrderStatus != OrderStatus.Pending) // Only allow cancellation if pending
            {
                throw new InvalidOperationException($"Order with ID {orderId} cannot be cancelled (not in Pending status).");
            }

            // Cancel the order
            order.OrderStatus = OrderStatus.Cancelled;
            order.ModifiedDate = DateTime.Now;
            await _orderRepository.UpdateAsync(order);

            // Update Ticket Quantities (Reverse the changes from Create)
            var orderItems = await _orderItemRepository.GetAllAsync();
            orderItems = orderItems.Where(oi => oi.OrderId == orderId).ToList();
            foreach (var orderItem in orderItems)
            {
                await _unitOfWork.TicketRepository.UpdateTicketQuantitySold(orderItem.TicketId, 0 - orderItem.Quantity);
            }

            await _unitOfWork.CompleteAsync();
            await transaction.CommitAsync();
            return orderId;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw; // Re-throw the exception for handling by the caller
        }
    }

    public async Task<int?> GetPendingEventOrderId(string userId, int eventId)
    {
        var order = await _orderRepository.GetOrderByUserAndEventIdAsync(userId, eventId);
        return order?.Id;
    }
}
