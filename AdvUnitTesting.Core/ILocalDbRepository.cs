using System.Threading.Tasks;

namespace AdvUnitTesting.Core
{
    public interface ILocalDbRepository
    {
        Task Save(UserModel user);
    }
}