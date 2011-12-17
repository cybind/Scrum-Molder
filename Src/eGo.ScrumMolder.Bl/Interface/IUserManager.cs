using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IUserManager
    {
        IEnumerable<User> GetAll();
        User Save(User user);
        bool Update(User client);
        User Get(Guid userId);

        User ValidateUser(string username, string password);
    }
}
