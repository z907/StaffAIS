using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class AddRecordVm:BaseVm
{
    private RecordService rService;
    private WorkshopService wService;
    private WorkerService wkService;

    private Record _newRecord = new Record();
    private List<Workshop> _workshops=new List<Workshop>();
    private List<Worker> _workers=new List<Worker>();

    private Workshop _selectedWorkshop;
    private Worker _selectedWorker;

    public Workshop SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
            UpdateData();
            OnPropertyChanged(nameof(SelectedWorkshop));
        }
    }
    
    public Worker SelectedWorker
    {
        get => _selectedWorker;
        set
        {
            _selectedWorker = value;
          
            OnPropertyChanged(nameof(SelectedWorker));
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

    public List<Worker> Workers
    {
        get => _workers;
        set
        {
            _workers = value;
           
            OnPropertyChanged(nameof(Workers));
        }
    }
    public Record NewRecord
    {
        get => _newRecord;
        set
        {
            _newRecord = value;
           
            OnPropertyChanged(nameof(NewRecord));
        }
    }
    private string _quantity="";

    public string Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }
    public AddRecordVm()
    {
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        rService = new RecordService();
        wService = new WorkshopService();
        wkService=new WorkerService();
        UpdateData();
    }
    
    private void UpdateData()
    {
        if(Workshops.Count==0)Workshops = wService.GetWorkshopsDefault();
        if (SelectedWorkshop!= null) Workers= wkService.GetWorkersByWNumber((int)SelectedWorkshop.Number);
    }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
            NewRecord.Quantity = Convert.ToInt32(Quantity);
            NewRecord.WorkerId = SelectedWorker.Id;
            rService.AddRecord(NewRecord);
            (obj as Window).DialogResult = true;
        }
    }

    private bool ValidateInput()
    {
        if (!InputValidator.ValidateNumberInput(Quantity) || SelectedWorkshop.StaffCap==null || SelectedWorker.HireDate==null)
        {
            CustomMessageBox.Show("Некорректно введены данные");
            return false;
        }
        if(rService.GetRecords().Exists(r=>r.Date==NewRecord.Date && r.WorkerName==SelectedWorker.Surname))
        {
            CustomMessageBox.Show("Запись уже есть, измените существующую");
            return false;
        }
            return true;
    }
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
}