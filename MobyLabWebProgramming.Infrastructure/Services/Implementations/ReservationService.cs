using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly WebAppDatabaseContext _databaseContext;

    public ReservationService(IRepository<WebAppDatabaseContext> repository, WebAppDatabaseContext databaseContext)
    {
        this._repository = repository;
        _databaseContext = databaseContext;
    }

    private Boolean hasIntersection(Reservation reservation1, ReservationAddDTO reservation2)
    {
        if( DateTime.Compare(reservation1.Start, reservation2.Start) <= 0 &&
            DateTime.Compare(reservation1.End, reservation2.End) >= 0) {
                return true;}

        if(DateTime.Compare(reservation1.Start, reservation2.Start) >= 0 &&
            DateTime.Compare(reservation1.End, reservation2.End) <= 0) { return true;}

        if(DateTime.Compare(reservation1.Start,reservation2.Start) <= 0 &&
            DateTime.Compare(reservation2.Start, reservation1.End) <= 0 ) {  return true;}

        if(DateTime.Compare(reservation1.Start,reservation2.End) <= 0 &&
            DateTime.Compare(reservation2.End, reservation1.End) <= 0 ) { return true;}

        return false;
    }

    private Boolean hasIntersection(Reservation reservation, DateTime time)
    {
        if( DateTime.Compare(reservation.Start,time) <= 0 &&
            DateTime.Compare(reservation.End,time) >= 0 ) { return true;}
        return false;
    }

    public async Task<ServiceResponse> AddReservation(ReservationAddDTO reservationAddDTO, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if(requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }
        var count = _databaseContext.Set<Reservation>()
            .AsEnumerable()
            .Where(e => e.TableId == reservationAddDTO.TableId &&
                hasIntersection(e, reservationAddDTO))
            .Count();
        var table = _databaseContext.Set<Table>()
            .Include(table => table.Location)
            .Where(e => e.Id == reservationAddDTO.TableId)
            .First();
        if (TimeOnly.FromDateTime(reservationAddDTO.Start).CompareTo(table.Location.OpeningHour) < 0 ||
            TimeOnly.FromDateTime(reservationAddDTO.End).CompareTo(table.Location.ClosingHour) > 0)
        {
            return ServiceResponse<ReservationDTO>.FromError(CommonErrors.LocationClosed);
        }else if(count == 0)
        {
            await _repository.AddAsync( new Reservation { Start = reservationAddDTO.Start, End = reservationAddDTO.End, optionalDetails = reservationAddDTO.optionalDetails, TableId = reservationAddDTO.TableId,
            Table = table, UserId = requestingUser.Id} );
            return ServiceResponse.ForSuccess();
        }
        else if(count < table.Quantity)
        {
            await _repository.AddAsync( new Reservation { Start = reservationAddDTO.Start, End = reservationAddDTO.End, optionalDetails = reservationAddDTO.optionalDetails, TableId = reservationAddDTO.TableId,
                Table = table, UserId = requestingUser.Id
            });
            return ServiceResponse.ForSuccess();
        }
        return ServiceResponse.FromError(CommonErrors.TableNotAvailable);
    }

    public async Task<ServiceResponse> DeleteReservation(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if(requestingUser == null)
        {
           return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }
        var reservation = _databaseContext.Set<Reservation>()
            .Where(reservation => reservation.Id == id)
            .Where(reservation => reservation.UserId == requestingUser.Id)
            .First();

        _databaseContext.Remove(reservation);
        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<List<ReservationDTO>>> GetAvailableReservations(Guid tableId, DateOnly day, CancellationToken cancellationToken = default)
    {
        var table = _databaseContext.Set<Table>()
            .Include(table => table.Location)
            .Include(table => table.Reservations)
            .Where(e => e.Id == tableId)
            .First();
        var tableDTO = await _repository.GetAsync(new TableProjectionSpec(tableId), cancellationToken);
        List<ReservationDTO> listAvailable = new List<ReservationDTO>();
        int index = 0;
        bool startedInterval = false;
        for (var i = table.Location.OpeningHour; table.Location.ClosingHour.CompareTo(i) > 0; i = i.AddMinutes(1))
        {
           var count = table.Reservations
                .AsEnumerable()
                .Where(reservation => hasIntersection(reservation, day.ToDateTime(i)))
                .Count();
            if(count < table.Quantity && !startedInterval)
            {
                listAvailable.Add(new ReservationDTO { Start = day.ToDateTime(i), Table = tableDTO });
                startedInterval = true;
            }
            if(count >= table.Quantity && startedInterval)
            {
                listAvailable.Last().End = day.ToDateTime(i);
                startedInterval = false;
            }
        }
        if(startedInterval) { listAvailable.Last().End = day.ToDateTime(table.Location.ClosingHour); }
        return ServiceResponse<List<ReservationDTO>>.ForSuccess(listAvailable);
    }

    public Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
