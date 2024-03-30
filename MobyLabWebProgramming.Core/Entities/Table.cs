namespace MobyLabWebProgramming.Core.Entities;

public class Table : BaseEntity
{
    public string Location { get; set; } = default!;
   // public Guid LocationId { get; set; } = default!;
    public ICollection<Reservation> Reservations { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Quantity { get; set; } = default!;


}
