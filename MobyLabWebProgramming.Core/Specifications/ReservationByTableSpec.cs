using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ReservationByTableSpec : BaseSpec<ReservationByTableSpec, Reservation>
{
    public ReservationByTableSpec(Guid tableId)
    {
        Query.Where(e => e.TableId == tableId);
    }
}
