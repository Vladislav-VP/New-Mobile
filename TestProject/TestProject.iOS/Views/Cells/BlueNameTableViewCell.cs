using System;

using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Plugin.Color.Platforms.Ios;
using UIKit;

using TestProject.Resources;
using TestProject.iOS.Extensions;

namespace TestProject.iOS.Views.Cells
{
    public class BlueNameTableViewCell : BaseTableViewCell
    {
        private UILabel _lblName;

        public BlueNameTableViewCell(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateView()
        {
            base.CreateView();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            _lblName = new UILabel
            {
                TextColor = AppColors.MainInterfaceBlue.ToNativeColor(),
                Font = UIFont.SystemFontOfSize(Constants.ListFontSize, UIFontWeight.Bold)
            };

            BackgroundColor = UIColor.Clear;
            ContentView.AddSubview(_lblName);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.DelayBind(
                () =>
                {
                    this.AddBindings(_lblName, Constants.TodoListItemBindingText);
                });
        }

        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                _lblName.AtLeftOf(ContentView, Constants.Padding),
                _lblName.AtTopOf(ContentView, Constants.Padding),
                _lblName.AtBottomOf(ContentView, Constants.Padding),
                _lblName.AtRightOf(ContentView, Constants.Padding)
            );
        }
    }   
}