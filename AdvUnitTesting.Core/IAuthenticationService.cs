using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string userName, string password);
    }
}