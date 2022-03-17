using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models;

namespace Online_Store.Services
{
   public interface INotConfirmUserService
    {
       Task<IEnumerable<NotConfirmUser>> GetAsync();
       Task<NotConfirmUser> GetAsync(Guid id);
        Task CreateAsync(NotConfirmUser notConfirmUser);
        Task Delete(Guid id);
        Task Update(NotConfirmUser notConfirmUser);
        bool Check(string login, string email);
    }

   public class NotConfirmUserService : INotConfirmUserService
   {
       private readonly AppDataContext _appDataContext;
       private readonly IEmailService _emailService;

       public NotConfirmUserService(AppDataContext appDataContext, IEmailService emailService)
       {
           _appDataContext = appDataContext;
           _emailService = emailService;
       }

       public async Task<IEnumerable<NotConfirmUser>> GetAsync()
       {
           return await _appDataContext.NotConfirmUsers.ToListAsync();
       }

       public async Task<NotConfirmUser> GetAsync(Guid id)
       {
           return await _appDataContext.NotConfirmUsers.FirstOrDefaultAsync(user => user.Id == id);
       }
       public bool Check(string login, string email)
       {
           if (_appDataContext.NotConfirmUsers.FirstOrDefault(u=>u.Login==login && u.Email==email) != null) return true;
           return false;
       }

        public async Task CreateAsync(NotConfirmUser notConfirmUser)
       {

           notConfirmUser.Role = _appDataContext.Roles.FirstOrDefault(role => role.Name == "GUEST");

            Random rd = new Random();
            var scoreNumber = "";
            var scoreNumber2 = "";
            var scoreChar = "";
            for (int i = 0; i < 5; i++)
            {
                scoreNumber += rd.Next(0, 9);
                scoreNumber2 += rd.Next(0, 9);
                scoreChar += Convert.ToChar(rd.Next(65, 90));
            }
            string message = scoreNumber + "-" + scoreChar + "-" + scoreNumber2;
               notConfirmUser.Code =message ;
               IEmailService mailService = new EmailService();
               await mailService.SendEmailAsync(notConfirmUser.Email
                   , "Confirm Account and Email", notConfirmUser.Code);
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
