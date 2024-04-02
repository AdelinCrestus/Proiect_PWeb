using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
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

    public LocationProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.

    }
}
