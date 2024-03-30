using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class LocationProjectionSpec : BaseSpec<LocationProjectionSpec, Location, LocationDTO>
{
    protected override Expression<Func<Location, LocationDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Address = e.Address,
        OpeningHour = e.OpeningHour,
        ClosingHour = e.ClosingHour
    };

    public LocationProjectionSpec(Guid id) : base(id) { }
}
