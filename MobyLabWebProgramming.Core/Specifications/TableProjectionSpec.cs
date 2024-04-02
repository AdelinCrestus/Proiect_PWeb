using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TableProjectionSpec : BaseSpec<TableProjectionSpec, Table, TableDTO>
{
    protected override Expression<Func<Table, TableDTO>> Spec => e => new()
    {
        Id = e.Id,
       // Location = e.Location,
        Location = new()
        {
            Id = e.Location.Id,
            Name = e.Location.Name,
            Address = e.Location.Address,
            OpeningHour = e.Location.OpeningHour,
            ClosingHour = e.Location.ClosingHour
        },
        Description = e.Description,
        Quantity = e.Quantity
    };

    public TableProjectionSpec(Guid id) : base(id) { }

    public TableProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Location.Name, searchExpr));
    }
}
