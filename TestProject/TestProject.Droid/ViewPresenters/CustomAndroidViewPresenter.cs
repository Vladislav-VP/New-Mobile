using System.Collections.Generic;
using System.Reflection;

using Android.Content;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace TestProject.Droid.ViewPresenters
{
    public class CustomAndroidViewPresenter : MvxAppCompatViewPresenter
    {
        public CustomAndroidViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }

        // TODO: Clean up CreateIntentForRequest method or remove class at all
        protected override Intent CreateIntentForRequest(MvxViewModelRequest request)
        {
            Intent intent = base.CreateIntentForRequest(request);
            request.PresentationValues = new Dictionary<string, string> { { "ClearBackStack", "True" } };
            if (request.PresentationValues != null)
            {
                if (request.PresentationValues.ContainsKey("ClearBackStack") && request.PresentationValues["ClearBackStack"] == "True")
                {
                    intent.AddFlags(ActivityFlags.ClearTop);
                }
            }
            return intent;
        }
    }
}