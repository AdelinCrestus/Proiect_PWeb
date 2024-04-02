using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ReservationAddDTO
{
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public Guid TableId { get; set; } = default!;
    public string optionalDetails { get; set; } = default!;

}
