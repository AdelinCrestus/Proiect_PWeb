namespace MobyLabWebProgramming.Core.Entities;

public class Location : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public TimeOnly OpeningHour { get; set; } = default!;
    public TimeOnly ClosingHour { get; set; } = default!;
    public ICollection<Table> Tables { get; set; } = default!;
}
