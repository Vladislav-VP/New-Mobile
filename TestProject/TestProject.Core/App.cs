using MvvmCross;
using MvvmCross.ViewModels;
using TestProject.Core.Services;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<ICalculationService, CalculationService>();

            RegisterAppStart<TipViewModel>();
        }
    }
}
