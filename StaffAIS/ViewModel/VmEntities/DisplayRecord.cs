using System.ComponentModel;
using Model.Entities;

namespace ViewModel.VmEntities;

public class DisplayRecord
{
    public int Id { get; set; }
   
    [DisplayName("Дата")]
    public DateOnly? Date { get; set; }

    [DisplayName("Фамилия")]
    public string WorkerName { get; set; }
    
    [DisplayName("Кол-во")]
    public int? Quantity { get; set; }
    
}