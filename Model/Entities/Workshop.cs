using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class Workshop
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? StaffCap { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
