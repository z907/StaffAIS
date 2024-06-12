using System.ComponentModel;

namespace ViewModel.VmEntities;

public class DisplayWorker
{
    public int Id { get; set; }
    [DisplayName("Имя")]
    public string? Name { get; set; }
    [DisplayName("Фамилия")]
    public string? Surname { get; set; }
    [DisplayName("Отчество")]
    public string? LastName { get; set; }
    [DisplayName("Дата найма")]
    public DateOnly? HireDate { get; set; }
    [DisplayName("Номер цеха")]
    public int? WorkshopNumber{ get; set; }
    [DisplayName("Ставка")]
    public int? Wage{ get; set; }
}