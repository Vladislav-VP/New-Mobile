using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;
using MvvmCross.Navigation;
using TestProject.Repositories;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) 
            : base(application, navigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<MainViewModel>();
        }

        protected async override Task<object> ApplicationStartup(object hint = null)
        {
            await new BaseRepository().CreateDatabase();
            return base.ApplicationStartup(hint);
        }
    }
}
