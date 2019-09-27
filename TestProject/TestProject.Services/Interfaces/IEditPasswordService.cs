using System.Threading.Tasks;

using TestProject.Services.DataHandleResults;

namespace TestProject.Services.Interfaces
{
    public interface IEditPasswordService
    {
        Task ChangePassword(EditPasswordResult result);
    }
}
