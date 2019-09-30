using System.Threading.Tasks;

using TestProject.Services.DataHandleResults;

namespace TestProject.Services.Interfaces
{
    public interface IEditUsernameService
    {
        Task EditUsername(EditUsernameResult result);
    }
}
