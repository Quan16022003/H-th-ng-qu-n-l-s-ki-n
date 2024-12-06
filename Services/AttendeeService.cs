using Constracts.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Services.Abtractions;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AttendeeService : IAttendeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AttendeeService> _logger;
        public AttendeeService(IUnitOfWork unitOfWork, ILogger<AttendeeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }
        public async Task AddAttendeeAsync(AttendeeDTO attendeeDTO)
        {
            try
            {
                _logger.LogInformation("Creating new attendee: {@CreateAttendeeDto}", attendeeDTO);
                var _attendee = attendeeDTO.Adapt<Attendees>();
                await _unitOfWork.AttendeeRepository.AddAsync(_attendee);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Attendee created successfully with id: {AttendeeId}", _attendee.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating attendee: {@AttendeeDTO}", attendeeDTO);
                throw new AttendeeCreationException(ex.Message, ex);
            }
        }

        public async Task DeleteAttendeeAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting attendee with id: {AttendeeId}", id);
                var _attendee = await _unitOfWork.AttendeeRepository.GetByIdAsync(id);
                if (_attendee == null)
                {
                    _logger.LogWarning("Attendee with id: {AttendeeId} was not found for deletion", id);
                    throw new AttendeeNotFoundException(id);
                }

                await _unitOfWork.AttendeeRepository.SoftDeleteAsync(_attendee);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Attendee with id: {AttendeeId} deleted successfully", id);
            }
            catch (Exception ex) when (ex is not AttendeeNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while deleting attendee with id: {AttendeeId}", id);
                throw new AttendeetDeletionException(id, ex.Message, ex);
            }
        }

        public async Task<IEnumerable<AttendeeDTO>> GetAllAttendeeAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all attendees");
                var _attendees = await _unitOfWork.AttendeeRepository.GetAllAsync();
                _attendees = _attendees.Where(c => c.IsDeleted = false).ToList();
                return _attendees.Adapt<IEnumerable<AttendeeDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all attendees");
                throw;
            }
        }

        public async Task<AttendeeDTO> GetAttendeeByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

            try
            {
                _logger.LogInformation("Fetching attendee with id: {AttendeeId}", id);

                var _attendee = await _unitOfWork.AttendeeRepository.GetByIdAsync(id);

                if (_attendee is null)
                {
                    _logger.LogWarning("Attendee not found");
                    throw new AttendeeNotFoundException(id);
                }

                return _attendee.Adapt<AttendeeDTO>();
            }
            catch (Exception ex) when (ex is not AttendeeNotFoundException
                                           and not ArgumentOutOfRangeException)
            {
                _logger.LogError(ex, "Error occurred while fetching attendee with id: {AttendeeId}", id);
                throw;
            }
        }

        public async Task UpdateAttendeeAsync(AttendeeDTO attendeeDTO)
        {
            try
            {
                _logger.LogInformation("Updating attendee with id: {AttendeeId}", attendeeDTO.Id);
                var _attendee = await _unitOfWork.AttendeeRepository.GetByIdAsync(attendeeDTO.Id);
                if (_attendee == null)
                {
                    _logger.LogWarning("Attendee with id: {AttendeeId} was not found for update", attendeeDTO.Id);
                    throw new AttendeeNotFoundException(attendeeDTO.Id);
                }

                attendeeDTO.Adapt(_attendee);
                await _unitOfWork.AttendeeRepository.UpdateAsync(_attendee);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Attendee with id: {AttendeeId} updated successfully", attendeeDTO.Id);
            }
            catch (Exception ex) when (ex is not AttendeeNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating attendee: {@AttendeeDTO}", attendeeDTO);
                throw new AttendeeUpdateException(attendeeDTO.Id, ex.Message, ex);
            }
        }

        public async Task UpdateCheckinAttendeeAsync(CheckinAttendeeDTO checkinAttendeeDTO)
        {
            try
            {
                _logger.LogInformation("Updating check in attendee with id: {AttendeeId}", checkinAttendeeDTO.Id);
                var _attendee = await _unitOfWork.AttendeeRepository.GetByIdAsync(checkinAttendeeDTO.Id);
                if (_attendee == null)
                {
                    _logger.LogWarning("Check in attendee with id: {AttendeeId} was not found for update", checkinAttendeeDTO.Id);
                    throw new CheckInAttendeeNotFoundException(checkinAttendeeDTO.Id);
                }
                if (_attendee.EventId != checkinAttendeeDTO.EventId)
                {
                    _logger.LogWarning("Check in attendee has mismatched eventId");
                    throw new CheckInAttendeeMismatchedEventIdException(checkinAttendeeDTO.Id);
                }

                checkinAttendeeDTO.Adapt(_attendee);
                await _unitOfWork.AttendeeRepository.UpdateAsync(_attendee);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Check in attendee with id: {AttendeeId} updated successfully", checkinAttendeeDTO.Id);
            }
            catch (Exception ex) when (ex is not CheckInAttendeeNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating check in attendee: {@AttendeeDTO}", checkinAttendeeDTO);
                throw new CheckInAttendeeUpdateException(checkinAttendeeDTO.Id, ex.Message, ex);
            }
        }

        public async Task UpdateIsCancelledAttendeeAsync(IsCancelledAttendeeDTO isCancelledAttendeeDTO)
        {
            try
            {
                _logger.LogInformation("Updating attendee is cancelled with id: {AttendeeId}", isCancelledAttendeeDTO.Id);
                var _attendee = await _unitOfWork.AttendeeRepository.GetByIdAsync(isCancelledAttendeeDTO.Id);
                if (_attendee == null)
                {
                    _logger.LogWarning("Attendee is cancelled with id: {AttendeeId} was not found for update", isCancelledAttendeeDTO.Id);
                    throw new IsCancelledAttendeeNotFoundException(isCancelledAttendeeDTO.Id);
                }

                isCancelledAttendeeDTO.Adapt(_attendee);
                await _unitOfWork.AttendeeRepository.UpdateAsync(_attendee);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation(" Attendee is cancelled with id: {AttendeeId} updated successfully", isCancelledAttendeeDTO.Id);
            }
            catch (Exception ex) when (ex is not IsCancelledAttendeeNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating attendee is cancelled: {@AttendeeDTO}", isCancelledAttendeeDTO);
                throw new IsCancelledAttendeeUpdateException(isCancelledAttendeeDTO.Id, ex.Message, ex);
            }
        }
    }
}
