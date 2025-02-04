using AutoMob_WebAPI.Models;

namespace AutoMob_WebAPI.Repository
{
    public interface IUserRepository
    {
        UserModel Authenticate(string username, string password);
        void Register(UserModel user);
    }
}
