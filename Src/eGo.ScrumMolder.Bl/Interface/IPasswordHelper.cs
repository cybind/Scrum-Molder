namespace eGo.ScrumMolder.Bl.Interface
{
    public interface IPasswordHelper
    {
        string CreatePasswordHash(string password, string salt);
        string CreateSalt();
        string GenerateRandomPassword(); 
    }
}