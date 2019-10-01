using System.Threading.Tasks;

using TestProject.Services.Interfaces;

namespace TestProject.iOS.Services
{
    public class CancelDialogService : ICancelDialogService
    {
        private const int _delayTime = 600;

        public async Task GoBack()
        {
            await Task.Delay(_delayTime);
        }
    }
}