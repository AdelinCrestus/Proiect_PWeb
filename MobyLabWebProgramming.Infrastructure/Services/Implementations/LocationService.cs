using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class LocationService : ILocationService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public LocationService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddLocation(LocationAddDTO locationDTO, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add a location!", ErrorCodes.CannotDelete));
        }

        await _repository.AddAsync(new Location { Address = locationDTO.Address, Name = locationDTO.Name, OpeningHour = TimeOnly.FromDateTime(locationDTO.OpeningHour), ClosingHour = TimeOnly.FromDateTime(locationDTO.ClosingHour) }, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteLocation(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the location!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Location>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<LocationDTO>> GetLocation(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new LocationProjectionSpec(id), cancellationToken);
        return result != null ?
            ServiceResponse<LocationDTO>.ForSuccess(result) :
            ServiceResponse<LocationDTO>.FromError(CommonErrors.LocationNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<LocationDTO>>> GetLocations(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new LocationProjectionSpec(pagination.Search), cancellationToken);
        return ServiceResponse<PagedResponse<LocationDTO>>.ForSuccess(result);


    }
}
