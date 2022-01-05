using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
   public interface IUserService
   {
       Task<IEnumerable<User>> GetAsync();
       User GetAsyncOne(string Email, string password);
       Task CreateAsync(NotConfirmUser notConfirmUser);
       ClaimsIdentity Authenticate(User user);
   }

   public class UserService:IUserService
   {
       private readonly AppDataContext _appDataContext;

       public UserService(AppDataContext appDataContext)
       {
           _appDataContext = appDataContext;
       }

       public async Task<IEnumerable<User>> GetAsync()
       {
           return await _appDataContext.Users.ToListAsync();
       }

       public User GetAsyncOne(string Email, string password)
       {
         return  _appDataContext.Users.FirstOrDefault(user => user.Email == Email && user.Password == password);
       }

       public  Task CreateAsync(NotConfirmUser notConfirmUser)
       {
           User user=new User
           {
               Id = notConfirmUser.Id,
               Name = notConfirmUser.Name,
               Email = notConfirmUser.Email,
               Password = notConfirmUser.Password,
               Role =  _appDataContext.Roles.FirstOrDefault(role =>role.RoleIndex==1),
               Surname = notConfirmUser.Surname,
               DataBorn = notConfirmUser.DataBorn,
               Number = notConfirmUser.Number,
               Year = DateTime.Now.Year-notConfirmUser.Year,
           };
           _appDataContext.Users.AddAsync(user);
            return  _appDataContext.SaveChangesAsync();
       }
       public ClaimsIdentity Authenticate(User user)
       {
           // создаем один claim
           var claims = new List<Claim>
           {
               new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
               new Claim("Role",_appDataContext.Roles.FirstOrDefault(r=>r.Id==user.RoleId).RoleIndex.ToString())
           };
           // создаем объект ClaimsIdentity
           ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
               ClaimsIdentity.DefaultRoleClaimType);
           return id;
       }
    }
}
