using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Model.Entities;
using StaffAIS.Controls;
using StaffAIS.Global;
using StaffAIS.Services;
using ViewModel;
using StaffAIS.Services;
using StaffAIS.View.Controls;

namespace StaffAIS.ViewModel;

public class MainVm:BaseVm
{
    private string _userName;
    private bool _isUserAdmin;
    private UserService logServ=new UserService();
    public bool IsUserAdmin
    {
        get => _isUserAdmin;
        set
        {
            _isUserAdmin = value;
            OnPropertyChanged(nameof(IsUserAdmin));
        }
    }
   
    public ICommand ShowTodayListCommand
    {
        get;
    }
    public ICommand ShowCheckIns
    {
        get;
    }
    public ICommand OccupancyReport
    {
        get;
    }
    public ICommand IncReport
    {
        get;
    }
    public ICommand ShowRooms
    {
        get;
    }
    public ICommand ShowUsers
    {
        get;
    }

    public ICommand ShowWorkshops
    {
        get;
    }
    public ICommand ShowPlans
    {
        get;
    }
    public ICommand ShowRecords
    {
        get;
    }
    public ICommand ShowWorkers
    {
        get;
    }
    public ICommand ShowProdReport
    {
        get;
    }
    public ICommand ShowPlanReport
    {
        get;
    }
    public MainVm()
    {
        UserName = Thread.CurrentPrincipal.Identity.Name;
        ShowUsers=new VmCommand(ShowUsersCommand,CanShowUsersCommand);
        ShowWorkshops=new VmCommand(ShowWorkshopsCommand,CanShowWorkshopsCommand);
        ShowPlans=new VmCommand(ShowPlansCommand,CanShowPlansCommand);
        ShowRecords = new VmCommand(ShowRecordsCommand, CanShowRecordsCommand);
        ShowWorkers = new VmCommand(ShowWorkersCommand,CanShowWorkersCommand);
        ShowProdReport = new VmCommand(ShowProdReportCommand,CanShowProdReportCommand);
        ShowPlanReport = new VmCommand(ShowPlanReportCommand,CanShowPlanReportCommand);
        IsUserAdmin = CheckRole();
    }

    private void ShowPlanReportCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        PlanReport item = new PlanReport();
        gr.Children.Add(item);
    }
    private bool CanShowPlanReportCommand(object obj)
    {
        return true;
    }
    private void ShowProdReportCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        ProdReport item = new ProdReport();
        gr.Children.Add(item);
    }
    private bool CanShowProdReportCommand(object obj)
    {
        return true;
    }
    
    private void ShowWorkersCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        WorkersGrid item = new WorkersGrid();
        gr.Children.Add(item);
    }
    private bool CanShowWorkersCommand(object obj)
    {
        return true;
    }
    private void ShowWorkshopsCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        WorkshopsGrid item = new WorkshopsGrid();
        gr.Children.Add(item);
    }
    private bool CanShowWorkshopsCommand(object obj)
    {
        return true;
    }
    private void ShowPlansCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        PlanGrid item = new PlanGrid();
        gr.Children.Add(item);
    }
    private bool CanShowPlansCommand(object obj)
    {
        return true;
    }
    private void ShowRecordsCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        RecordGrid item = new RecordGrid();
        gr.Children.Add(item);
    }
    private bool CanShowRecordsCommand(object obj)
    {
        return true;
    }
    
    
    
    
    private bool CheckRole()
    {
       return Thread.CurrentPrincipal.IsInRole("Администратор");
    }
    private void ShowUsersCommand(object obj)
    {
        Grid gr = obj as Grid;
        gr.Children.Clear();
        UserGrid item = new UserGrid();
        gr.Children.Add(item);
    }
    private bool CanShowUsersCommand(object obj)
    {
        return true;
    }
    
    
    public string UserName
    {
        get => _userName;
        set => _userName = value;
    }
}