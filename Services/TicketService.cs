using Constracts.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Services.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace Services
{
    internal sealed class TicketService: ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventService> _logger;
        public TicketService(IUnitOfWork unitOfWork, ILogger<EventService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
          
        }

        public async Task AddTicketAsync(TicketDTO ticketDTO)
        {
            try
            {
                _logger.LogInformation("Creating new ticket: {@CreateTicketDto}", ticketDTO);
                var _ticket = ticketDTO.Adapt<Tickets>();
                await _unitOfWork.TicketRepository.AddAsync(_ticket);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Ticket created successfully with id: {TicketId}", _ticket.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating ticket: {@TicketDTO}", ticketDTO);
                throw new TicketCreationException(ex.Message, ex);
            }
        }

        public async Task DeleteTicketAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting ticket with id: {TicketId}", id);
                var _ticket = await _unitOfWork.TicketRepository.GetByIdAsync(id);
                if (_ticket == null)
                {
                    _logger.LogWarning("Ticket with id: {TicketId} was not found for deletion", id);
                    throw new TicketNotFoundException(id);
                }

                await _unitOfWork.TicketRepository.SoftDeleteAsync(_ticket);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Ticket with id: {TicketId} deleted successfully", id);
            }
            catch (Exception ex) when (ex is not TicketNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while deleting ticket with id: {TicketId}", id);
                throw new TicketDeletionException(id, ex.Message, ex);
            }
        }

        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all tickets");
                var _tickets = await _unitOfWork.TicketRepository.GetAllAsync();
                _tickets = _tickets.Where(c => c.IsDeleted = false).ToList();
                return _tickets.Adapt<IEnumerable<TicketDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all tickets");
                throw;
            }
        }

        public async Task<IEnumerable<TicketDTO>> GetAllTicketsByEventIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            try
            {
                _logger.LogInformation("Fetching all tickets by id event");
                var _tickets = await _unitOfWork.TicketRepository.GetAllAsync();
                _tickets = _tickets.Where(c => c.EventId == id).ToList();
                return _tickets.Adapt<IEnumerable<TicketDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all tickets by id event");
                throw;
            }
        }

        public async Task<TicketDTO> GetTicketByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

            try
            {
                _logger.LogInformation("Fetching ticket with id: {TicketId}", id);

                var _ticket = await _unitOfWork.TicketRepository.GetByIdAsync(id);

                if (_ticket is null)
                {
                    _logger.LogWarning("Ticket not found");
                    throw new TicketNotFoundException(id);
                }

                return _ticket.Adapt<TicketDTO>();
            }
            catch (Exception ex) when (ex is not TicketNotFoundException
                                           and not ArgumentOutOfRangeException)
            {
                _logger.LogError(ex, "Error occurred while fetching ticket with id: {TicketId}", id);
                throw;
            }
        }

        public async Task UpdateTicketAsync(TicketDTO ticketDTO)
        {
            try
            {
                _logger.LogInformation("Updating ticket with id: {TicketId}", ticketDTO.Id);
                var _ticket = await _unitOfWork.TicketRepository.GetByIdAsync(ticketDTO.Id);
                if (_ticket == null)
                {
                    _logger.LogWarning("Ticket with id: {TicketId} was not found for update", ticketDTO.Id);
                    throw new TicketNotFoundException(ticketDTO.Id);
                }

                ticketDTO.Adapt(_ticket);
                await _unitOfWork.TicketRepository.UpdateAsync(_ticket);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Ticket with id: {TicketId} updated successfully", ticketDTO.Id);
            }
            catch (Exception ex) when (ex is not TicketNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating ticket: {@TicketDTO}", ticketDTO);
                throw new TicketUpdateException(ticketDTO.Id, ex.Message, ex);
            }
        }
    }
}
