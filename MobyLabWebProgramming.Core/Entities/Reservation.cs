using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities;

public class Reservation : BaseEntity
{
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
    public string optionalDetails { get; set; } = default!;
    public Guid TableId { get; set; }
    public Table Table { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    //Todo: Add UserId
}
