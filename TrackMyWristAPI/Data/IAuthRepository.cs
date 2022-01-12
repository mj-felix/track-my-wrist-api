using System.Threading.Tasks;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Data
{
    public interface IAuthRepository
    {
        Task<AuthServiceResponse> Register(User user, string password);
        Task<string> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}