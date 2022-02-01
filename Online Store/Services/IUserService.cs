using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.ViewModels;

namespace Online_Store.Services
{
   public interface IUserService
   {
       Task<IEnumerable<User>> GetAsync();
       Task<User> GetOne(string login, string password);
       Task CreateAsync(NotConfirmUser notConfirmUser);
       Task<ClaimsIdentity> Authenticate(User user);
       Task<User> GetAsyncOne(string login);
       Task Update(EditUserViewModel editUser);
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

       public async Task<User>  GetOne(string login, string password)
       {
         return await _appDataContext.Users.FirstOrDefaultAsync(user => user.Login == login && user.Password == password);
       }

       public async Task<User> GetAsyncOne(string login)
       {
           return await _appDataContext.Users.FirstOrDefaultAsync(user => user.Login == login);
       }
       public async Task CreateAsync(NotConfirmUser notConfirmUser)
       {
           User user=new User
           {
               Id = notConfirmUser.Id,
               Login = notConfirmUser.Login,
               Name = notConfirmUser.Name,
               Email = notConfirmUser.Email,
               Password = notConfirmUser.Password,
               Role =  _appDataContext.Roles.FirstOrDefault(role =>role.Name == "USER"),
               Surname = notConfirmUser.Surname,
               DataBorn = notConfirmUser.DataBorn,
               Number = notConfirmUser.Number,
               Year = DateTime.Now.Year-notConfirmUser.DataBorn.Year,
           };
             await _appDataContext.Users.AddAsync(user);
             await _appDataContext.SaveChangesAsync();
       }

       public async Task Update(EditUserViewModel editUser)
       {
           var user = _appDataContext.Users.FirstOrDefault(user1 => user1.Id == editUser.Id);
           user.Email =editUser.Email;
           user.Name = editUser.Name;
           user.Surname = editUser.Surname;
            user.DataBorn = editUser.DataBorn;
            user.Number = editUser.Number;
           user.Year = DateTime.Now.Year - user.DataBorn.Year;
          _appDataContext.Users.Update(user);
         await Authenticate(user);
          await _appDataContext.SaveChangesAsync();
       }
       public async Task<ClaimsIdentity> Authenticate(User user)
       {
           user.Role = await _appDataContext.Roles.FirstOrDefaultAsync(role =>role.Id==user.RoleId);
           // создаем один claim
           var claims = new List<Claim>
           {
               new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
               new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.Name),
           };
           // создаем объект ClaimsIdentity
           ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
               ClaimsIdentity.DefaultRoleClaimType);
           return id;
       }
    }
}
