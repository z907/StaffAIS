using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using CsvHelper;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Microsoft.Win32;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using ViewModel;
using ViewModel.VmEntities;

namespace StaffAIS.ViewModel;

public partial class PlanReportVm : BaseVm
{
     private SeriesCollection _reportData;
    private ReportService repService;
    public ICommand GetReport { get; }
    public ICommand SaveReport { get; }
    
    private List<int> _yearList ;
    
    public List<int> YearList
    {
        get => _yearList;
        set
        {
            _yearList = value;
            OnPropertyChanged(nameof(YearList));
        }
    }
    
   
    private int statYear;

    public int StatYear
    {
        get => statYear;
        set
        {
            statYear = value;
            OnPropertyChanged(nameof(StatYear));
        }
    }
    private WorkshopService wService;
    private WorkerService wkService;
    public PlanReportVm()
    {
        GetReport = new VmCommand(ExecuteGetReport,CanExecuteGetReport);
        SaveReport = new VmCommand(ExecuteSaveReport,CanExecuteSaveReport);
        LoadData();
        repService = new ReportService();
        wService = new WorkshopService();
        wkService = new WorkerService();
       
    }

    private void LoadData()
    {
        YearList = DateDict.YearList;
    }
    public SeriesCollection ReportData
    {
        get => _reportData;
        set
        {
            _reportData = value;
            OnPropertyChanged(nameof(ReportData));
        }
    }

    private string[] _barLabels;

    public string[] BarLabels
    {
        get => _barLabels;
        set
        {
            _barLabels = value;
            OnPropertyChanged(nameof(BarLabels));
        }
    }

    private List<PlanReportData> data_plan;
    private List<PlanReportData> data_actual;
    private List<int> _values1;
    public List<int> Values1
    {
        get => _values1;
        set
        {
            _values1 = value;
            OnPropertyChanged(nameof(Values1));
        }
    }
    private List<int> _values2;
    public List<int> Values2
    {
        get => _values2;
        set
        {
            _values2 = value;
            OnPropertyChanged(nameof(Values2));
        }
    }
    public void ExecuteGetReport(object obj)
    {
        if ( statYear == 0 ) return;
        data_plan = repService.GetPlanInYear(statYear);
        data_actual = repService.GetActualInYear(statYear);
        Formatter = value => value.ToString("N");
        
       Values1 = data_plan.Select(inc => inc.Quantity).ToList();
       Values2 = data_actual.Select(inc => inc.Quantity).ToList();
        List<string> months = DateDict.Months.Select(inc => inc.Value).ToList();
        
        ReportData = new SeriesCollection
        {
           new LineSeries()
           {
               Title = "Кол-во",
               Values = new ChartValues<ObservableValue>()
           },
           new LineSeries()
           {
               Title = "План",
               Values = new ChartValues<ObservableValue>()
           }
        };
        foreach(var x in Values2)
        ReportData.First().Values.Add(new ObservableValue(x));
        foreach(var x in Values1) 
            ReportData.Last().Values.Add(new ObservableValue(x));
        
        BarLabels = months.ToArray();
    }

    private Func<double, string> _formatter;

    public Func<double, string> Formatter
    {
        get => _formatter;
        set
        {
            _formatter = value;
            OnPropertyChanged(nameof(Formatter));
        }
    }
    public bool CanExecuteGetReport(object obj)
    {
        return true;
    }
    public void ExecuteSaveReport(object obj)
    {
        SaveFileDialog fileDialog = new SaveFileDialog();
        fileDialog.Filter = "CSV отчет|*.csv";
        if (fileDialog.ShowDialog() == true)
        {
            using (var writer=new StreamWriter(fileDialog.FileName,false,Encoding.GetEncoding(65001)))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteField(StatYear.ToString());
                csv.WriteField(" год");
                csv.NextRecord();
                csv.WriteField("Месяц");
                csv.WriteField("План");
                csv.WriteField("Выход");
                csv.NextRecord();
                for (int i = 1; i <= 12; i++)
                {
                    csv.WriteField(DateDict.Months[i]);
                    csv.WriteField(Values1[i-1].ToString());
                    csv.WriteField(Values2[i-1].ToString());
                    csv.NextRecord();
                }
            }
        }
    }
    public bool CanExecuteSaveReport(object obj)
    {
        return data_plan!=null;
    }
}