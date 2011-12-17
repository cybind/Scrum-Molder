using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eGo.ScrumMolder.Bl.Interface;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Bl
{
    public class UserManager : BaseManager, IUserManager
    {
        private readonly IPasswordHelper _passwordHelper;

        public UserManager(IPasswordHelper passwordHelper)
        {
            _passwordHelper = passwordHelper;
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _context.Users.All();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
        }

        public User Save(User user)
        {
            try
            {
                if (_context.Users.EntityChange(user))
                {
                    if (_context.Users.Update(user) == 0)
                        return user;
                }
                else
                {
                    var dbUser = _context.Users.Find(u=>u.Email.ToLower() == user.Email.ToLower());
                    if (dbUser == null)
                    {
                        user.Id = Guid.NewGuid();
                        user.PasswordSalt = _passwordHelper.CreateSalt();
                        user.Password = _passwordHelper.CreatePasswordHash(user.Password, user.PasswordSalt);
                        user.UserType = Enums.UserType.Unknow;

                        return _context.Users.Create(user);    
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
                return null;
            }
            
            return null;
        }

        public bool Update(User client)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid userId)
        {
            throw new NotImplementedException();
        }

        public User ValidateUser(string email, string password)
        {
            try
            {
                var user = _context.Users.Find(u => u.Email.ToLower() == email.ToLower());
                if (user != null)
                {
                    var hashPassword = _passwordHelper.CreatePasswordHash(password, user.PasswordSalt);
                    if (hashPassword == user.Password)
                        return user;
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Exception: {0}", ex.ToString());
            }

            return null;
        }
    }
}
