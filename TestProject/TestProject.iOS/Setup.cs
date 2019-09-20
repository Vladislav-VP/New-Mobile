using MvvmCross.Platforms.Ios.Core;
using MvvmCross;
using TestProject.Core;
using MvvmCross.Base;
using MvvmCross.Plugin.Json;
using MvvmCross.IoC;
using MvvmCross.Converters;
using TestProject.iOS.Converters;

namespace TestProject.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("ImageValue", new ImageValueConverter());
        }
    }
}