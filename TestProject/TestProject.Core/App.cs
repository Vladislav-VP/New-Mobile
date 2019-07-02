using MvvmCross;
using MvvmCross.ViewModels;
using TestProject.Core.ViewModels;

namespace TestProject.Core
{
    
public class App : MvxApplication
    {
        public override void Initialize()
        {

            RegisterAppStart<TodoListItemViewModel>();
        }
    }
}
