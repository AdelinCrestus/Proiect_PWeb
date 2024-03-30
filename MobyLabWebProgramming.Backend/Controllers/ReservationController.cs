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
public class ReservationController : AuthorizedController
{
    private readonly IReservationService _reservationService;
    public ReservationController(IUserService userService, IReservationService reservationService) : base(userService)
    {
        _reservationService = reservationService;
    }

    [Authorize, HttpGet("{tableId:guid}")]
    public async Task<ActionResult<RequestResponse<PagedResponse<ReservationDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination, [FromRoute] Guid tableId)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.GetAvailableReservations(pagination, tableId)) :
            this.ErrorMessageResult<PagedResponse<ReservationDTO>>(currentUser.Error);
    }

    [Authorize, HttpPost]
    public async Task<ActionResult<RequestResponse>> MakeReservation([FromForm] ReservationAddDTO form)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.AddReservation(form)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize, HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> CancelReservation([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.DeleteReservation(id)) : 
            this.ErrorMessageResult(currentUser.Error);
    }
}
