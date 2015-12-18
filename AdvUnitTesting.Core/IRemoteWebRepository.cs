using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public interface IRemoteWebRepository
    {
        Task<UserModel> LoadUser(int userId);
    }
}