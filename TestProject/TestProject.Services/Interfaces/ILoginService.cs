using System.Threading.Tasks;

using TestProject.Services.DataHandleResults;

namespace TestProject.Services.Interfaces
{
    public interface ILoginService
    {
        Task Login(LoginResult result);
    }
}
