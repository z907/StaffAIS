using System.Windows;
using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class EditRecordVm:BaseVm
{
    private RecordService rService;
    private WorkshopService wService;
    private WorkerService wkService;

    private DateOnly _date;
    private Record _newRecord = new Record();
    private List<Workshop> _workshops=new List<Workshop>();
    private List<Worker> _workers=new List<Worker>();

    private Workshop _selectedWorkshop;
    private Worker _selectedWorker;

    private string _workshopNumber;
    private string _workerName;

    public string WorkshopNumber
    {
        get => _workshopNumber;
        set
        {
            _workshopNumber = value;
            OnPropertyChanged(nameof(WorkshopNumber));
        }
    }

    public string WorkerName
    {
        get => _workerName;
        set
        {
            _workerName = value;
            OnPropertyChanged(nameof(WorkerName));
        }
    }

    public Workshop SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
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
    public DateOnly Date
    {
        get => _date;
        set
        {
            _date = value;
           
            OnPropertyChanged(nameof(Date));
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
    public EditRecordVm()
    {
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        rService = new RecordService();
        wService = new WorkshopService();
        wkService=new WorkerService();
       
    }
    
    public void UpdateData(int recordId)
    {
        _newRecord.Id = recordId;
        WorkerName=wkService.GetWorkerName(recordId);
        WorkshopNumber = wService.GetWorkshopNumber(recordId).ToString();
        Date = (DateOnly)rService.GetRecords().Where(r=>r.Id==recordId).First().Date;
        Quantity = rService.GetRecords().Where(r => r.Id == recordId).First().Quantity.ToString();
    }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
          
            _newRecord.Quantity=Convert.ToInt32(Quantity);
            _newRecord.Date = _date;
            rService.EditRecord(_newRecord);
            (obj as Window).DialogResult = true;
        }
    }

    private bool ValidateInput()
    {
        if (!InputValidator.ValidateNumberInput(Quantity) )
        {
            CustomMessageBox.Show("Некорректно введены данные");
            return false;
        }
        if(rService.GetRecords().Exists(r=>r.Date==Date && r.WorkerName==WorkerName))
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