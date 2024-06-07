using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class PlanGridVm:BaseVm
{
    private List<Plan> _plans;
    private PlanService pService;
    private Plan _selectedPlan;
    public List<Plan> Plans
    {
        get => _plans;
        set
        {
            _plans = value;
            OnPropertyChanged(nameof(Plans));
        }
    }
    public Plan SelectedPlan
    {
        get => _selectedPlan;
        set
        {
            _selectedPlan = value;
            OnPropertyChanged(nameof(SelectedPlan));
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
    public PlanGridVm()
    {
        Edit = new VmCommand(ExecuteEditCommand, CanExecuteEditCommand);
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        Delete = new VmCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        pService = new PlanService();
        Plans = pService.GetPlans();
    }
    
    private void ExecuteDeleteCommand(object obj)
    {
        DeleteConfirmationDialog deleteDialog = new DeleteConfirmationDialog();
        if (deleteDialog.ShowDialog() == true)
        {
            pService.DeletePlan((int)_selectedPlan.Id);
            Plans = pService.GetPlans();
        }
    }

    private bool CanExecuteDeleteCommand(object obj)
    {
        return _selectedPlan!=null;
    }
    private void ExecuteEditCommand(object obj)
    {
        EditPlanDialog win = new EditPlanDialog(_selectedPlan.Id);
        if (win.ShowDialog() == true)
        {
            Plans = pService.GetPlans();
        }
    }

    private bool CanExecuteEditCommand(object obj)
    {
        return _selectedPlan!=null;
    }
    private void ExecuteAddCommand(object obj)
    {
        AddPlanDialog win = new AddPlanDialog();
        if (win.ShowDialog() == true)
        {
            Plans = pService.GetPlans();
        }
    }

    private bool CanExecuteAddCommand(object obj)
    {
        return true;
    }
}