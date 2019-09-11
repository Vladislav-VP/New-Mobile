using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCross.Platforms.Ios.Core;
using Foundation;
using MvvmCross;
using UIKit;
using TestProject.Core;
using MvvmCross.Base;
using MvvmCross.Plugin.Json;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;

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

    }
}