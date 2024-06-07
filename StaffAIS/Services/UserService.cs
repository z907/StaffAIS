using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

using ViewModel.VmEntities;


namespace StaffAIS.Services;

public class UserService:BaseService
{
   public UserService():base()
   {
      
   }

   public VmUser CheckPassword(string username,string password)
   {
     db.UserRoles.Load();
      var ul = db.Users.Where(u => u.Login == username && u.Password == password);
      if (!ul.Any()) return null;
         var ru = new VmUser(ul.First());
         return ru;
   }

   public string GetRole(string login)
   {
       db.UserRoles.Load();
       return db.Users.First(u => u.Login == login).RoleNavigation.Role;
   }
   public void AddUser(VmUser user)
   {
       var u = new User{Login = user.Login,Password = user.Password,Role = user.Role};
       db.Users.Add(u);
       db.SaveChanges();
   }
   public void DeleteUser(int userId)
   {
       var u = db.Users.Find(userId);
       db.Users.Remove(u);
       db.SaveChanges();
   }
   public List<VmUser> GetUsers()
   {
       db.UserRoles.Load();
       return db.Users.ToList().Select(u=>new VmUser(u)).ToList();
   }
   public List<UserRole> GetRoles()
   {
       return db.UserRoles.ToList();
   }
}