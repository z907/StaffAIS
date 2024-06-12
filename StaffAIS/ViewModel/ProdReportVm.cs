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

public partial class ProdReportVm : BaseVm
{
     private SeriesCollection _reportData;
    private ReportService repService;
    public ICommand GetReport { get; }
    public ICommand SaveReport { get; }
    
    private List<int> _yearList ;
    
    private Workshop? _selectedWorkshop=null;
    private List<Workshop> _workshops;
    public Workshop SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
            UpdateWorkerList();
            OnPropertyChanged(nameof(SelectedWorkshop));
        }
    }
    public List<Workshop> Workshops
    {
        get => _workshops;
        set
        {
            _workshops = value;
          
            OnPropertyChanged(nameof(Workshops));
        }
    }
    public List<int> YearList
    {
        get => _yearList;
        set
        {
            _yearList = value;
            OnPropertyChanged(nameof(YearList));
        }
    }

    private List<Worker> _workers;
    private Worker _selectedWorker=null;
    
    public Worker SelectedWorker
    {
        get => _selectedWorker;
        set
        {
            _selectedWorker = value;
            OnPropertyChanged(nameof(SelectedWorker));
        }
    }
    public List<Worker> Workers
    {
        get => _workers;
        set
        {
            _workers = value;
            OnPropertyChanged(nameof(Workers));
        }
    }

    private Worker AllWorkers;
    private void UpdateWorkerList()
    {
        if (SelectedWorkshop != null)
        {
            Workers = wkService.GetWorkersPlusAll((int)SelectedWorkshop.Number);
        }
    }
    private Dictionary<int, string> _months;

    public Dictionary<int, string> Months
    {
        get => _months;
        set
        {
            _months = value;
            OnPropertyChanged(nameof(Months));
        }
    }
    
    private int statMonth;

    public int StatMonth
    {
        get => statMonth;
        set
        {
            statMonth = value;
            OnPropertyChanged(nameof(StatMonth));
        }
    }
    private int statYear;

    public int StatYear
    {
        get => statYear;
        set
        {
            statYear = value;
            OnPropertyChanged(nameof(StatMonth));
        }
    }
    private WorkshopService wService;
    private WorkerService wkService;
    public ProdReportVm()
    {
        GetReport = new VmCommand(ExecuteGetReport,CanExecuteGetReport);
        SaveReport = new VmCommand(ExecuteSaveReport,CanExecuteSaveReport);
        LoadData();
        repService = new ReportService();
        wService = new WorkshopService();
        wkService = new WorkerService();
        Workshops = wService.GetWorkshopsDefault();
    }

    private void LoadData()
    {
        Months = DateDict.Months;
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

    private List<ProdReportData> data;
    private List<int> _values;
    public List<int> Values
    {
        get => _values;
        set
        {
            _values = value;
            OnPropertyChanged(nameof(Values));
        }
    }
    public void ExecuteGetReport(object obj)
    {
        if (statMonth == 0 || statYear == 0 || SelectedWorker==null) return;
        data = repService.GetProdPerDay(statMonth,statYear,SelectedWorker.Id,(int)SelectedWorkshop.Number);
        
        Formatter = value => value.ToString("N");
        
       Values = data.Select(inc => inc.Quantity).ToList();
        List<string> days = data.Select(inc => inc.Day.ToString()).ToList();
      
        ReportData = new SeriesCollection
        {
           new LineSeries()
           {
               Title = "Кол-во",
               Values = new ChartValues<ObservableValue>()
           }
        };
        foreach(var x in Values)
        ReportData.First().Values.Add(new ObservableValue(x));
        
        BarLabels = days.ToArray();
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
                csv.WriteField(Months[StatMonth]);
                csv.WriteField(StatYear.ToString());
                csv.NextRecord();
                csv.WriteField(SelectedWorkshop.Number.ToString()+ " цех");
                csv.WriteField(SelectedWorker.Surname);
                csv.NextRecord();
                csv.WriteRecords(data);
                csv.WriteField("Итого");
                csv.WriteField(data.Sum(r => r.Quantity).ToString());
            }
        }
    }
    public bool CanExecuteSaveReport(object obj)
    {
        return data!=null;
    }
}