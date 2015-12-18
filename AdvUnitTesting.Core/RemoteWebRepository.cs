using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public class RemoteWebRepository : IRemoteWebRepository
    {
        public async Task<UserModel> LoadUser(int userId)
        {
            return await Task.FromResult(new UserModel
            {
                Id = 1,
                FirstName = "Rob",
                LastName = "Gibbens"
            });
        }
    }
}