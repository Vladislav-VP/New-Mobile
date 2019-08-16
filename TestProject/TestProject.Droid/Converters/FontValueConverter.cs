using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Converters;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace TestProject.Droid.Converters
{
    public class FontValueConverter : MvxValueConverter<bool, Font.Style>
    {
        protected override Font.Style Convert(bool value, System.Type targetType, object parameter, CultureInfo culture)
        {
            // TODO : Write convertion logic
            return base.Convert(value, targetType, parameter, culture);
        }
    }
}