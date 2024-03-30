using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ReservationService : IReservationService
{
    public Task<ServiceResponse> AddReservation(ReservationAddDTO reservationAddDTO, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse> DeleteReservation(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<PagedResponse<ReservationDTO>>> GetAvailableReservations(PaginationSearchQueryParams pagination, Guid tableId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
