using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class AddPlanVm:BaseVm
{
    private PlanService pService;
    private Plan _newPlan=new Plan();
    private Dictionary<int, string> _months;
    private List<int> _years;

    public Dictionary<int, string> Months
    {
        get => _months;
        set
        {
            _months = value;
            OnPropertyChanged(nameof(Months));
        }
    }

    public List<int> Years
    {
        get => _years;
        set
        {
            _years = value;
           
            OnPropertyChanged(nameof(Years));
        }
    }
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
    public AddPlanVm()
    {
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        pService = new PlanService();
        LoadData();
    }

    private void LoadData()
    {
        Months = DateDict.Months;
        Years = DateDict.YearList;
    }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
            _newPlan.Quantity = Convert.ToInt32(Quantity);
            pService.AddPlan(NewPlan);
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

        if (pService.GetPlans().Exists(p => p.Month == NewPlan.Month && p.Year == NewPlan.Year))
        {
            CustomMessageBox.Show("План уже есть");
            return false;
        }
        return true;
    }
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
}