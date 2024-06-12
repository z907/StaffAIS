using System.Windows;
using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class AddWorkerVm:BaseVm
{
    private WorkerService wkService;
    private WorkshopService wService;
    private Worker _newWorker=new Worker { Name = "",Surname = "",LastName = ""};

    private Workshop _selectedWorkshop=null;

    public Workshop SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
            OnPropertyChanged(nameof(SelectedWorkshop));
        }
    }

    private List<Workshop> _workshops;
    public List<Workshop> Workshops
    {
        get => _workshops;
        set
        {
            _workshops = value;
            OnPropertyChanged(nameof(Workshops));
        }
    }
    public Worker NewWorker
    {
        get => _newWorker;
        set
        {
            _newWorker = value;
            OnPropertyChanged(nameof(NewWorker));
        }
    }
    public AddWorkerVm()
    {
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        wkService = new WorkerService();
        wService = new WorkshopService();
        LoadData();
    }

    private void LoadData()
    {
        Workshops = wService.GetWorkshopsDefault();
    }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
            _newWorker.WorkshopId = SelectedWorkshop.Id;
            _newWorker.HireDate=DateOnly.FromDateTime(DateTime.Now);
            wkService.AddWorker(NewWorker);
            (obj as Window).DialogResult = true;
        }
    }

    private bool ValidateInput()
    {
        if (!InputValidator.ValidateNumberInput(NewWorker.Wage.ToString()) || NewWorker.Name==""
                 || NewWorker.Surname==""
                 || NewWorker.LastName=="" ||SelectedWorkshop==null)
        {
            CustomMessageBox.Show("Некорректно введены данные");
            return false;
        }

        var a=wService.GetWorkshops().First(w => w.Id == SelectedWorkshop.Id);
        if(a.StaffPresent==a.StaffCap) 
        {
            CustomMessageBox.Show("В цеху нет мест");
            return false;
        }
        return true;
    }
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
}