using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IReservationService
{
    public Task<ServiceResponse<List<ReservationDTO>>> GetAvailableReservations(Guid tableId, DateOnly day, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddReservation(ReservationAddDTO reservationAddDTO, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteReservation(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
