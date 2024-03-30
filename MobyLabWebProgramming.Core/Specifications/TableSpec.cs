using Table = MobyLabWebProgramming.Core.Entities.Table;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TableSpec : BaseSpec<TableSpec, Table>
{
    public TableSpec(Guid id) : base(id) { }
}
