using System.Collections.Generic;
using System.Reflection;

using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Widget;
using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Converters;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Presenters;

using TestProject.Core;
using TestProject.Droid.Converters;
using TestProject.Droid.Helpers;
using TestProject.Droid.Helpers.Interfaces;
using TestProject.Droid.MvxBinds;
using TestProject.Droid.Services;
using TestProject.Services.Interfaces;

namespace TestProject.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies
            => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(MvxRecyclerView).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(Toolbar).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(NavigationView).Assembly,
            typeof(BottomNavigationView).Assembly,
            typeof(MvxSwipeRefreshLayout).Assembly
        };

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            IBitmapConvertionHelper bitmapConvertionHelper = Mvx.IoCProvider.Resolve<IBitmapConvertionHelper>();
            registry.AddOrOverwrite("ImageValue", new ImageValueConverter(bitmapConvertionHelper));
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);

            var factory = new MvxCustomBindingFactory<SwipeRefreshLayout>("IsRefreshing", (swipeRefreshLayout)
                => new SwipeRefreshLayoutIsRefreshingTargetBinding(swipeRefreshLayout));
            registry.RegisterFactory(factory);
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }
        
        protected override IMvxIoCProvider InitializeIoC()
        {
            IMvxIoCProvider provider = base.InitializeIoC();

            Mvx.IoCProvider.RegisterSingleton(typeof(IBitmapConvertionHelper), new BitmapConvertionHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(IActivityReplaceHelper), new ActivityReplaceHelper());
            Mvx.IoCProvider.RegisterSingleton(typeof(ICancelDialogService), new CancelDialogService());

            return provider;
        }
    }
}