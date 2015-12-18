using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public class AuthenticationService
    {
        public AuthenticationService(string userName, string password)
        {
        }

        public async Task<bool> Login()
        {
            return await Task.FromResult(true);
        }
    }
}