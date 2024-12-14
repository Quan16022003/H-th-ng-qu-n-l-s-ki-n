using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    /// <summary>
    /// Encapsulates all repository transactions.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Properties

        public UserManager<ApplicationUser> UserManager { get; set; }
        public ICategoryEventRepository CategoryEventRepository { get; }
        public IEventRepository EventRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
        public IOrderTicketRepository OrderTicketRepository { get; }
        public IAttendeeRepository AttendeeRepository { get; }

        #endregion

        #region Readonlys

        private readonly RepositoryDbContext _dbContext;
        private IDbContextTransaction? _currentTransaction;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext">The Database Context</param>
        /// <param name="userManager">User Manager</param>
        /// <param name="categoryEventRepository">The Category Events Repository</param>
        /// <param name="eventRepository">The Event Repository</param>
        /// <param name="ticketRepository">The Ticket Repository</param>
        /// <param name="orderRepository">The Order Repository</param>
        /// <param name="orderItemRepository">The Order Item Repository</param>
        /// <param name="orderTicketRepository">The Order Ticket Repository</param>
        /// <param name="attendeeRepository">The Attendee Repository</param>
        public UnitOfWork(
            RepositoryDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ICategoryEventRepository categoryEventRepository,
            IEventRepository eventRepository,
            ITicketRepository ticketRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IOrderTicketRepository orderTicketRepository,
            IAttendeeRepository attendeeRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            UserManager = userManager;
            CategoryEventRepository = categoryEventRepository ?? throw new ArgumentNullException(nameof(categoryEventRepository));
            EventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            TicketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            OrderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            OrderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            OrderTicketRepository = orderTicketRepository ?? throw new ArgumentNullException(nameof(orderTicketRepository));
            AttendeeRepository = attendeeRepository ?? throw new ArgumentNullException(nameof(attendeeRepository));
        }

        #region Methods

        /// <summary>
        /// Completes the unit of work, saving all repository changes to the underlying data-store.
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task CompleteAsync() => await _dbContext.SaveChangesAsync();
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        #endregion
    }
}