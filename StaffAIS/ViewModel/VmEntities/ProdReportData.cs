using CsvHelper.Configuration.Attributes;

namespace ViewModel.VmEntities;

public class ProdReportData
{
    [Name("День")]
    [Index(0)]
    public int Day { get; set; }
    [Name("Кол-во")]
    [Index(1)]
    public int Quantity { get; set; }
}