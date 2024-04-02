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

public class TableService : ITableService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public TableService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> DeleteTable(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete a table!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Table>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<TableDTO>> GetTable(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new TableProjectionSpec(id), cancellationToken);
        return result != null ?
            ServiceResponse<TableDTO>.ForSuccess(result) :
            ServiceResponse<TableDTO>.FromError(CommonErrors.TableNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<TableDTO>>> GetTables(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TableProjectionSpec(pagination.Search), cancellationToken);
        return ServiceResponse<PagedResponse<TableDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> SaveTable(TableAddDTO tableAddDTO, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add a table!", ErrorCodes.CannotDelete));
        }
        Location tableLocation = await _repository.GetAsync(new LocationSpec(tableAddDTO.LocationId));
       await _repository.AddAsync( new Table { Location = tableLocation ,LocationId = tableAddDTO.LocationId, Description = tableAddDTO.Description, Quantity = tableAddDTO.Quantity }, cancellationToken);
       return ServiceResponse.ForSuccess();
    }
}
