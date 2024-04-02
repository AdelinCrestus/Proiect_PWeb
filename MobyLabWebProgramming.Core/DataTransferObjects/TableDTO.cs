namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TableDTO
{
    public Guid Id { get; set; }
    public LocationDTO Location { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}
