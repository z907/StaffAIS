using System.Windows;
using System.Windows.Input;
using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

namespace StaffAIS.ViewModel;

public class AddWorkshopVm:BaseVm
{
    public AddWorkshopVm()
    {
        wService = new WorkshopService();
        OK = new VmCommand(ExecuteOkCommand, CanExecuteOkCommand);
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
   
    
    public ICommand OK { get; }

    private void ExecuteOkCommand(object obj)
    {
        if (ValidateInput())
        {
            wService.AddWorkshop(new Workshop
            {
                Number = Convert.ToInt32(Num),
                StaffCap = Convert.ToInt32(Cap)
            });
            (obj as Window).DialogResult = true;
        }
        else CustomMessageBox.Show("Некорректно введены данные");
    }

    private bool ValidateInput()
    {
        if (Cap == "" || Num == "") return false;
        
        foreach (var c in Cap)
        {
            if (!char.IsDigit(c)) return false;
        }
        foreach (var c in Num)
        {
            if (!char.IsDigit(c)) return false;
        }
        if (Convert.ToInt32(Cap)==0 || Convert.ToInt32(Num)==0) return false;
        if (wService.GetWorkshops().Exists(w => w.Number == Convert.ToInt32(Num))) return false;
    return true;
}
    private bool CanExecuteOkCommand(object obj)
    {
        return true;
    }
}