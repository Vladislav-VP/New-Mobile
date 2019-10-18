using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProject.Forms.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormsApp : Application
    {
        public FormsApp()
        {
            InitializeComponent();
        }
    }
}