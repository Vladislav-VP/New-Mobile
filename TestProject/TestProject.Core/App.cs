using MvvmCross;
using MvvmCross.ViewModels;
using TestProject.Core.Services.Interfaces;
using TestProject.Core.Services.Implementations;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{
    
public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IObjectiveService, ObjectiveService>();

            RegisterAppStart<ListItemViewModel>();
        }
    }
}
