using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;
using ViewModel.VmEntities;

namespace StaffAIS.ViewModel;

public class RecordGridVm:BaseVm
{
    private List<DisplayRecord> _records;
    private RecordService rService;
    private WorkshopService wService;
    private DisplayRecord _selectedRecord;
    private List<Workshop> _workshops;
    private Workshop _selectedWorkshop;
    private DateOnly? _startDate=null;
    private DateOnly? _endDate=null;

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
    
    public Workshop SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
            UpdateRecords();
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
    public List<DisplayRecord> Records
    {
        get => _records;
        set
        {
            _records = value;
            OnPropertyChanged(nameof(Records));
        }
    }
    public DisplayRecord SelectedRecord
    {
        get => _selectedRecord;
        set
        {
            _selectedRecord = value;
            OnPropertyChanged(nameof(SelectedRecord));
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
    public RecordGridVm()
    {
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        Delete = new VmCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        rService = new RecordService();
        wService = new WorkshopService();
        UpdateRecords();
        Workshops = wService.GetWorkshopsDefault();
    }

    private void UpdateRecords()
    {
        if (!EndDate.HasValue || !StartDate.HasValue || SelectedWorkshop == null) return;
        Records = rService.GetRecordsByDate(StartDate.Value,EndDate.Value,SelectedWorkshop.Id);
    }
    private void ExecuteDeleteCommand(object obj)
    {
        DeleteConfirmationDialog deleteDialog = new DeleteConfirmationDialog();
        if (deleteDialog.ShowDialog() == true)
        {
            rService.DeleteRecord((int)_selectedRecord.Id);
            UpdateRecords();
        }
    }

    private bool CanExecuteDeleteCommand(object obj)
    {
        return _selectedRecord!=null;
    }
    private void ExecuteEditCommand(object obj)
    {
        EditRecordDialog win = new EditRecordDialog(_selectedRecord.Id);
        if (win.ShowDialog() == true)
        {
            UpdateRecords();
        }
    }

    private bool CanExecuteEditCommand(object obj)
    {
        return _selectedRecord!=null;
    }
    private void ExecuteAddCommand(object obj)
    {
        AddRecordDialog win = new AddRecordDialog();
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