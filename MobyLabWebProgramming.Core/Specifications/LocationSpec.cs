using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class LocationSpec : BaseSpec<LocationSpec, Location>
{
    public LocationSpec(Guid id) :base(id) { }
}
