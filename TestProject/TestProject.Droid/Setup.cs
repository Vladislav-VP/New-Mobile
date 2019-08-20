using System.Collections.Generic;
using System.Reflection;

using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Widget;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Presenters;

using TestProject.Core;
using TestProject.Droid.Converters;

namespace TestProject.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(MvxRecyclerView).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(Toolbar).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(NavigationView).Assembly,
            typeof(BottomNavigationView).Assembly
        };

        protected override IEnumerable<Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies as IList<Assembly>;
                toReturn.Add(typeof(MvxValueConverter<string, Bitmap>).Assembly);
                toReturn.Add(typeof(ImageValueConverter).Assembly);
                return (IEnumerable<Assembly>)toReturn;
            }
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);
            registry.AddOrOverwrite("ImageValue", new ImageValueConverter());
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);

        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(this.AndroidViewAssemblies);
        }

       
    }
}