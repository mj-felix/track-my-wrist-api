using System.Threading.Tasks;
using TrackMyWristAPI.Models;

namespace TrackMyWristAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<AuthResponse> Register(User user, string password);
        Task<AuthResponse> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}