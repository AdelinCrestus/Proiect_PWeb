using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ILocationService
{
    public Task<ServiceResponse<PagedResponse<LocationDTO>>> GetLocations(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<LocationDTO>> GetLocation(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddLocation(LocationAddDTO locationDTO, UserDTO? requestingUser = default,  CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteLocation(Guid id, UserDTO? requestingUser = default , CancellationToken cancellationToken = default);
}
