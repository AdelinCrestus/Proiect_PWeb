using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ReservationProjectionSpec : BaseSpec<ReservationProjectionSpec, Reservation, ReservationDTO>
{
    protected override Expression<Func<Reservation, ReservationDTO>> Spec => e => new()
    {
        Id = e.Id,
        Start = e.Start,
        End = e.End,
        Table = new()
        {//TODO: nu cred ca e ok, trb cautat la ce deja exista in baza, daca nu exista adaug
            Id = e.Table.Id,
            Location = e.Table.Location, //new()
            /*{
                Id = e.Table.Location.Id,
                Name = e.Table.Location.Name,
                Address = e.Table.Location.Address,
                OpeningHour = e.Table.Location.OpeningHour,
                ClosingHour = e.Table.Location.ClosingHour
            },*/
            Quantity = e.Table.Quantity
        }
    };

    public ReservationProjectionSpec(Guid id) : base(id) { }


}
