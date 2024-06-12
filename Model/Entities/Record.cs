using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class Record
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? Date { get; set; }

    public int? WorkerId { get; set; }

    public virtual Worker? Worker { get; set; }
}
