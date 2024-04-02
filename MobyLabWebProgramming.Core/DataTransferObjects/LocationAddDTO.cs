using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class LocationAddDTO
{ 
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;

    [DataType(DataType.Time)]
    public DateTime OpeningHour { get; set; } = default!;

    [DataType(DataType.Time)]
    public DateTime ClosingHour { get; set; } = default!;
}
