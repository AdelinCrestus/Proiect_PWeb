using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationDTO
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public TableDTO Table { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


}
