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
    public async Task<ActionResult<RequestResponse<List<ReservationDTO>>>> GetAvailableReservations([FromRoute] Guid tableId, [FromQuery] DateTime day)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.GetAvailableReservations(tableId, DateOnly.FromDateTime(day) )) :
            this.ErrorMessageResult<List<ReservationDTO>>(currentUser.Error);
    }

    [Authorize, HttpPost]
    public async Task<ActionResult<RequestResponse>> MakeReservation([FromForm] ReservationAddDTO form)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.AddReservation(form, requestingUser: currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize, HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> CancelReservation([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        return currentUser.Result != null ?
            this.FromServiceResponse(await _reservationService.DeleteReservation(id, requestingUser: currentUser.Result)) : 
            this.ErrorMessageResult(currentUser.Error);
    }
}
