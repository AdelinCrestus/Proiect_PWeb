using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITableService
{
    public Task<ServiceResponse<PagedResponse<TableDTO>>> GetTables(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<TableDTO>> GetTable(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> SaveTable(TableAddDTO tableAddDTO, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteTable(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

}
