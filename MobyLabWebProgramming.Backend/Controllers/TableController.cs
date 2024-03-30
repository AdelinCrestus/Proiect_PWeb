using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TableController : AuthorizedController
{
    private readonly ITableService _tableService;
    public TableController(IUserService userService, ITableService tableService) : base(userService)
    {
        _tableService = tableService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<TableDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _tableService.GetTables(pagination)) :
            this.ErrorMessageResult<PagedResponse<TableDTO>>(currentUser.Error);
    }

    [Authorize, HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TableDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _tableService.GetTable(id)) :
            this.ErrorMessageResult<TableDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromForm] TableAddDTO form)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _tableService.SaveTable(form)) :
            this.ErrorMessageResult(currentUser.Error);

    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _tableService.DeleteTable(id, requestingUser: currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

}
