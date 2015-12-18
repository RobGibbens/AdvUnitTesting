using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public class AuthenticationService
    {
        private readonly string _userName;
        private readonly string _password;

        public AuthenticationService(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }

        public async Task<bool> Login()
        {
            if (string.IsNullOrWhiteSpace(_userName) || string.IsNullOrWhiteSpace(_password))
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
    }
}