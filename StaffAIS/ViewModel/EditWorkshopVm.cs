using System.Windows;
using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;
using ViewModel.VmEntities;

namespace StaffAIS.ViewModel;

public class EditWorkshopVm:BaseVm
{
    public EditWorkshopVm()
    {
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
        wService = new WorkshopService();
    }
    
    private WorkshopService wService;
    private string _num="";
    private string _cap="";
    
    public string Cap
    {
        get => _cap;
        set
        {
            _cap = value;
            OnPropertyChanged(nameof(Cap));
        }
    }
    public string Num
    {
        get => _num;
        set
        {
            _num = value;
            OnPropertyChanged(nameof(Num));
        }
    }

    private Workshop wToEdit;
    public void LoadData(int id)
    {
        wToEdit = wService.GetWorkshopById(id);
        Num = wToEdit.Number.ToString();
        Cap = wToEdit.StaffCap.ToString();
    }
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (!ValidateInput())
        {
            CustomMessageBox.Show("Некорректные данные");
            return;
        }
        wService.EditWorkshop(new DisplayWorkshop
        {
            Id=wToEdit.Id,
            StaffCap = Convert.ToInt32(Cap),
            Number = Convert.ToInt32(Num)
        });
            (obj as Window).DialogResult = true;
    }

  
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }

    private bool ValidateInput()
    {
        if (!InputValidator.ValidateNumberInput(Num) || !InputValidator.ValidateNumberInput(Cap)) return false;
        if (wService.GetWorkshops().Exists(w => w.Number == Convert.ToInt32(Num) && w.Id!=wToEdit.Id) ) return false;
        if(wService.GetWorkshops().Find(w=>w.Id==wToEdit.Id).StaffPresent>Convert.ToInt32(Cap)) return false;
        return true;
    }
}