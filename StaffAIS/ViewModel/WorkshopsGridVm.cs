using System.Windows.Input;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;
using ViewModel.VmEntities;

namespace StaffAIS.ViewModel;

public class WorkshopsGridVm:BaseVm
{

    public WorkshopsGridVm()
    {
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        Delete = new VmCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        wService = new WorkshopService();
        Workshops= wService.GetWorkshops();
    }
    private List<DisplayWorkshop> _workshops;
    private WorkshopService wService;
    private DisplayWorkshop _selectedWorkshop;

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
    public DisplayWorkshop SelectedWorkshop
    {
        get => _selectedWorkshop;
        set
        {
            _selectedWorkshop = value;
            OnPropertyChanged(nameof(SelectedWorkshop));
        } 
    }
    public List<DisplayWorkshop> Workshops
    {
        get => _workshops;
        set
        {
            _workshops = value;
            OnPropertyChanged(nameof(Workshops));
        } 
    }
    
    private void ExecuteDeleteCommand(object obj)
    {
        DeleteConfirmationDialog deleteDialog = new DeleteConfirmationDialog();
        if (deleteDialog.ShowDialog() == true)
        {
            if (!wService.DeleteWorkshop((int)_selectedWorkshop.Id))
            {
                CustomMessageBox.Show("Сначала удалите работников!");
                return;
            }
            Workshops = wService.GetWorkshops();
        }
    }

    private bool CanExecuteDeleteCommand(object obj)
    {
        return _selectedWorkshop!=null;
    }
    private void ExecuteEditCommand(object obj)
    {
            EditWorkshopDialog win = new EditWorkshopDialog(SelectedWorkshop.Id);
            if (win.ShowDialog() == true)
            {
                Workshops = wService.GetWorkshops();
            }
    }

    private bool CanExecuteEditCommand(object obj)
    {
        return _selectedWorkshop!=null;
    }
    private void ExecuteAddCommand(object obj)
    {
        AddWorkshopDialog win = new AddWorkshopDialog();
        if (win.ShowDialog() == true)
        {
            Workshops = wService.GetWorkshops();
        }
    }

    private bool CanExecuteAddCommand(object obj)
    {
        return true;
    }
    
  
}