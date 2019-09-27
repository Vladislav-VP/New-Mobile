using System.Threading.Tasks;

using TestProject.Services.DataHandleResults;

namespace TestProject.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task RegisterUser(RegistrationResult result);
    }
}
