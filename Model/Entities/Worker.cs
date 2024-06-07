using System;
using System.Collections.Generic;

namespace Model.Entities;

public partial class Worker
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? LastName { get; set; }

    public DateOnly? HireDate { get; set; }

    public int? WorkshopId { get; set; }

    public virtual Workshop? Workshop { get; set; }
}
