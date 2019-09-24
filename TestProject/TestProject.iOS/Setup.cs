using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Converters;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Json;

using TestProject.Core;
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
            var options = new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };

            return options;
        }

        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("ImageValue", new ImageValueConverter());
        }
    }
}