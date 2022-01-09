using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;
using Online_Store.Services;

namespace Online_Store.Services
{
   public interface INotConfirmUserService
    {
       Task<IEnumerable<NotConfirmUser>> GetAsync();
       Task<NotConfirmUser> GetAsync(Guid id);
        Task CreateAsync(NotConfirmUser notConfirmUser);
        Task Delete(Guid id);
        Task Update(NotConfirmUser notConfirmUser);
        bool Check(string name, string login);
    }

   public class NotConfirmUserService : INotConfirmUserService
   {
       private readonly AppDataContext _appDataContext;

       public NotConfirmUserService(AppDataContext appDataContext)
       {
           _appDataContext = appDataContext;
       }

       public async Task<IEnumerable<NotConfirmUser>> GetAsync()
       {
           return await _appDataContext.NotConfirmUsers.ToListAsync();
       }

       public async Task<NotConfirmUser> GetAsync(Guid id)
       {
           return await _appDataContext.NotConfirmUsers.FirstOrDefaultAsync(user => user.Id == id);
       }
       public bool Check(string name, string login)
       {
           if (_appDataContext.NotConfirmUsers.FirstOrDefault(u => u.Name == name) != null) return true;
           if (_appDataContext.NotConfirmUsers.FirstOrDefault(u => u.Login==login) != null) return true;
           return false;
       }

        public async Task CreateAsync(NotConfirmUser notConfirmUser)
       {

           notConfirmUser.Role = _appDataContext.Roles.FirstOrDefault(role => role.RoleIndex == 0);
          
               Random rdnumber = new Random();
               Random rdbukver = new Random();
               Random rdchange = new Random();
               string Code = "";
               while (Code.Length < 10)
               {
                   int rdch = rdchange.Next(0, 2);
                   if (rdch == 1) { Code += Convert.ToChar(rdbukver.Next(41, 67)); }
                   else { Code += rdnumber.Next(0,10).ToString(); }
               }
               string message=
               notConfirmUser.Code = Code;
               EmailService mailService = new EmailService();
               await mailService.SendEmailAsync(notConfirmUser.Email, "Confirm Account and Email", notConfirmUser.Code);
               await _appDataContext.AddAsync(notConfirmUser);
               await _appDataContext.SaveChangesAsync();
       }

       public async Task Delete(Guid id)
       {
           _appDataContext.NotConfirmUsers.Remove(_appDataContext.NotConfirmUsers.FirstOrDefault(user => user.Id == id));
          await _appDataContext.SaveChangesAsync();
       }

       public Task Update(NotConfirmUser notConfirmUser)
       {
          _appDataContext.NotConfirmUsers.Update(notConfirmUser);
          return _appDataContext.SaveChangesAsync();
       }
   }
}
