using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using TestProject.Core;

namespace TestProject.Droid
{
    [Application]
    public class MainApplication : MvxAndroidApplication<MvxAndroidSetup<App>, App>
    {
        public MainApplication(IntPtr javaReference,JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}