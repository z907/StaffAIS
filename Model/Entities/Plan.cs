using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Entities;

public partial class Plan
{
    public int Id { get; set; }
    [DisplayName("Месяц")]
    public string? Month { get; set; }
    [DisplayName("Год")]
    public string? Year { get; set; }
    [DisplayName("Количество")]
    public int? Quantity { get; set; }
}
