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
    private readonly IUnitOfWork _unitOfWork;

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
    public async Task<bool> ConfirmOrderAsync(int orderId)
    {
        // Tìm đơn hàng theo ID
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new Exception("Không tìm thấy đơn hàng.");

        if (order.OrderStatus != OrderStatus.Pending)
            throw new Exception("Đơn hàng không thể xác nhận vì không ở trạng thái chờ xử lý.");

        // Cập nhật trạng thái đơn hàng
        order.OrderStatus = OrderStatus.Confirmed;
        order.ModifiedDate = DateTime.Now;

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

}