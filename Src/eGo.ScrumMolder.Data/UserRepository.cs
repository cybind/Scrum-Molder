using eGo.ScrumMolder.Data.Interface;
using eGo.ScrumMolder.Dto;

namespace eGo.ScrumMolder.Data
{
    public class UserRepository : Repository<User>, IUserRepository 
    {
        public UserRepository(ScrumMolderContext context) : base(context) { }
    }
}