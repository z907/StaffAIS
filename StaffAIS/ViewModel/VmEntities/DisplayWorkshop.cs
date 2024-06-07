using System.ComponentModel;

namespace ViewModel.VmEntities;

public class DisplayWorkshop
{
    public int Id { get; set; }
    [DisplayName("Номер")]
    public int? Number { get; set; }
    [DisplayName("Макс. штат")]
    public int? StaffCap { get; set; }
    [DisplayName("Текущий")]
    public int? StaffPresent { get; set; }
    [DisplayName("Выход за последний месяц")]
    public int? LastMonthProduction { get; set; }
}