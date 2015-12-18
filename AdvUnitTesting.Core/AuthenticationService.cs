using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> Login(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
    }
}