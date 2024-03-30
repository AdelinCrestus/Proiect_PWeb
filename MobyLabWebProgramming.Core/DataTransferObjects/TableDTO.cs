using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TableDTO
{
    public Guid Id { get; set; }
    public string Location { get; set; }
    public ICollection<ReservationDTO> availableReservations { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}
