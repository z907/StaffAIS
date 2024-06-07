using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Entities;

namespace ViewModel.VmEntities;

public class VmUser
{
    public int Id { get; set; }
    [DisplayName("Логин")]
    public string? Login { get; set; }
    [DisplayName("Пароль")]
    public string? Password { get; set; }

    public int? Role { get; set; }
    [DisplayName("Роль")]
     public string? RoleName  { get; set; }
    public VmUser(){}

    public VmUser(User u)
    {
        Id = u.Id;
        Login= u.Login;
        Password = u.Password;
        Role = u.Role;
        RoleName = u.RoleNavigation.Role;
    }
    
}