using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Windows.Documents;
// using System.Windows.Forms;
using System.Windows.Input;
using CsvHelper;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
// using FileDialog = System.Windows.Forms.FileDialog;
// using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;
using ViewModel.VmEntities;

namespace StaffAIS.ViewModel;

public class WorkersGridVm:BaseVm
{
    private List<DisplayWorker> _workers;
    private RecordService rService;
    public WorkerService wkService;
    private WorkshopService wService;
    private DisplayWorker _selectedWorker;
    private Dictionary<int, string> _workshops;
    private string _selectedWorkshop="";
    private DateOnly? _startDate=null;
    private DateOnly? _endDate=null;
    private ReportService repService;

    public DateOnly? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            UpdateRecords();
            OnPropertyChanged(nameof(StartDate));
        }
    }
    public DateOnly? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            UpdateRecords();
            OnPropertyChanged(nameof(EndDate));
        }
    }
    
    public string SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
            UpdateRecords();
            OnPropertyChanged(nameof(SelectedWorkshop));
        }
    }
    public Dictionary<int, string> Workshops
    {
        get => _workshops;
        set
        {
            _workshops = value;
          
            OnPropertyChanged(nameof(Workshops));
        }
    }
    public List<DisplayWorker> Workers
    {
        get => _workers;
        set
        {
            _workers = value;
            OnPropertyChanged(nameof(Workers));
        }
    }
    public DisplayWorker SelectedWorker
    {
        get => _selectedWorker;
        set
        {
            _selectedWorker = value;
            OnPropertyChanged(nameof(SelectedWorker));
        }
    }
    public ICommand Add
    {
        get;
    }
    public ICommand Edit
    {
        get;
    }
    public ICommand Delete
    {
        get;
    }
    public ICommand Report
    {
        get;
    }
    public WorkersGridVm()
    {
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        Delete = new VmCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        Report = new VmCommand(ExecuteRecordCommand,CanExecuteRecordCommand);
        rService = new RecordService();
        wService = new WorkshopService();
        wkService = new WorkerService();
        repService = new ReportService();
        UpdateRecords();
        Workshops = wService.GetWorkshopsDict();
        Workshops.Add(0,"Все");
    }
    private void ExecuteRecordCommand(object obj)
    {
        var reports = repService.GetPaymentReports(StartDate.Value, EndDate.Value);
        SaveFileDialog fileDialog = new SaveFileDialog();
        fileDialog.Filter = "CSV отчет|*.csv";
        if (fileDialog.ShowDialog() == true)
        {
            using (var writer=new StreamWriter(fileDialog.FileName,false,Encoding.GetEncoding(65001)))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteField(StartDate.ToString());
                csv.WriteField(EndDate.ToString());
                csv.NextRecord();
                csv.WriteRecords(reports);
                csv.WriteField("Итого");
                csv.WriteField(reports.Sum(r => r.ToPay).ToString());
            }
        }
    }

    private bool CanExecuteRecordCommand(object obj)
    {
        return StartDate!=null&&EndDate!=null;
    }

    private void UpdateRecords()
    {
        if ( SelectedWorkshop == "") return;
        if (SelectedWorkshop != "Все") Workers = wkService.GetWorkersByWNumberD(Convert.ToInt32(SelectedWorkshop));
        else Workers = wkService.GetWorkersD();
    }
    private void ExecuteDeleteCommand(object obj)
    {
        DeleteConfirmationDialog deleteDialog = new DeleteConfirmationDialog();
        if (deleteDialog.ShowDialog() == true)
        {
            wkService.DeleteWorker((int)_selectedWorker.Id);
            UpdateRecords();
        }
    }

    private bool CanExecuteDeleteCommand(object obj)
    {
        return _selectedWorker!=null;
    }
    private void ExecuteEditCommand(object obj)
    {
        EditWorkerDialog win = new EditWorkerDialog(_selectedWorker.Id);
        if (win.ShowDialog() == true)
        {
            UpdateRecords();
        }
    }

    private bool CanExecuteEditCommand(object obj)
    {
        return _selectedWorker!=null;
    }
    private void ExecuteAddCommand(object obj)
    {
        AddWorkerDialog win = new AddWorkerDialog();
        if (win.ShowDialog() == true)
        {
            UpdateRecords();
        }
    }

    private bool CanExecuteAddCommand(object obj)
    {
        return true;
    }
}