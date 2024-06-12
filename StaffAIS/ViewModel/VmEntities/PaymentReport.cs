using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace ViewModel.VmEntities;

public class PaymentReport
{
    [Name("ФИО")]
    [Index(0)]
    public string FIO { get; set; }
    [Name("Цех")]
    [Index(1)]

    public int Workshop { get; set; }
  
    [Name("Выработка")]
    [Index(2)]
    public int Count { get; set; }
   
    [Name("Ставка")]
    [Index(3)]
    public int Wage { get; set; }
 
    [Name("К выплате")]
    [Index(4)]
    public int ToPay { get; set; }
    
}