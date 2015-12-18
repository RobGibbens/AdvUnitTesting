using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public class LocalDbRepository : ILocalDbRepository
    {
        public async Task Save(UserModel user)
        {
            await Task.Run(() => { });
        }
    }
}