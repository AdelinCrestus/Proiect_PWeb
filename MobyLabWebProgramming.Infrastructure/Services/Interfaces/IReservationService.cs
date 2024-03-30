using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IReservationService
{
    public Task<ServiceResponse<PagedResponse<ReservationDTO>>> GetAvailableReservations(PaginationSearchQueryParams pagination, Guid tableId , CancellationToken cancellationToken = default);
    public Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddReservation(ReservationAddDTO reservationAddDTO, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteReservation(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
