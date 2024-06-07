using System.Windows.Input;

using Model.Entities;
using StaffAIS.Global;
using StaffAIS.Services;
using StaffAIS.View.Windows;
using ViewModel;

using ViewModel.VmEntities;

namespace StaffAIS.ViewModel;

public class UserVm:BaseVm
{
    private List<VmUser> _users;
    private UserService userServ = new UserService();
    private List<UserRole> roles;

    public List<UserRole> Roles
    {
        get => roles;
        set
        {
            roles = value ;
            OnPropertyChanged(nameof(Roles));
        }
    }

    public List<VmUser> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }

    
    private VmUser _selectedUser;

    public VmUser SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged(nameof(SelectedUser));
        }
    }
    private VmUser _newUser=new VmUser();

    public VmUser NewUser
    {
        get => _newUser;
        set
        {
            _newUser = value;
            OnPropertyChanged(nameof(NewUser));
        }
    }

    public UserVm()
    {
        Add = new VmCommand(ExecuteAddCommand, CanExecuteAddCommand);
        Delete = new VmCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        Users = userServ.GetUsers();
        Roles = userServ.GetRoles();
    }
    private void ExecuteAddCommand(object obj)
    {
        AddUserDialog win = new AddUserDialog();
        win.DataContext = this;
        if (win.ShowDialog() == true)
        {
            userServ.AddUser(NewUser);
            Users = userServ.GetUsers();
        }
    }
    private bool CanExecuteAddCommand(object obj)
    {
        return true;
    }
    private void ExecuteDeleteCommand(object obj)
    {
      userServ.DeleteUser(SelectedUser.Id);
      Users = userServ.GetUsers();
    }

    private bool CanExecuteDeleteCommand(object obj)
    {
        return SelectedUser!=null;
    }
    public ICommand Add
    {
        get;
    }
    public ICommand Delete
    {
        get;
    }
}