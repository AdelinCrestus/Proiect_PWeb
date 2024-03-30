namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class LocationAddDTO
{ 
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public TimeOnly OpeningHour { get; set; } = default!;
    public TimeOnly ClosingHour { get; set; } = default!;
}
