namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class LocationDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public TimeOnly OpeningHour { get; set; } = default!;
    public TimeOnly ClosingHour { get; set; } = default!;
}
