using System.Windows;
using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class EditPlanVm:BaseVm
{
    private PlanService pService;
    private Plan _newPlan=new Plan();

   
    public Plan NewPlan
    {
        get => _newPlan;
        set
        {
            _newPlan = value;
           
            OnPropertyChanged(nameof(NewPlan));
        }
    }
    private string _quantity;

    public string Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged(nameof(Quantity));
        }
    }
    public EditPlanVm()
    {
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        pService = new PlanService();
    }

    public void LoadData(int id)
    {
        NewPlan = pService.GetPlanById(id);
        Quantity = NewPlan.Quantity.ToString();
    }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
            _newPlan.Quantity = Convert.ToInt32(Quantity);
            pService.EditPlan(NewPlan);
            (obj as Window).DialogResult = true;
        }
    }

    private bool ValidateInput()
    {
        if (!InputValidator.ValidateNumberInput(Quantity))
        {
            CustomMessageBox.Show("Некорректно введены данные");
            return false;
        }
        return true;
    }
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
}