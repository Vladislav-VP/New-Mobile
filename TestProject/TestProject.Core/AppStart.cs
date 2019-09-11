using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModels;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) 
            : base(application, navigationService)
        {
        }

        protected async override Task NavigateToFirstViewModel(object hint = null)
        {
            await NavigationService.Navigate<MainViewModel>();
        }
    }
}
