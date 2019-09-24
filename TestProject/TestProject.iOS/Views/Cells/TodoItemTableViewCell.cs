using System;

using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Plugin.Color.Platforms.Ios;
using UIKit;

using TestProject.Resources;

namespace TestProject.iOS.Views.Cells
{
    public class TodoItemTableViewCell : BaseTableViewCell
    {
        private UILabel _lblName;
        private UISwitch _swIsDone;

        public TodoItemTableViewCell(IntPtr handle) : base(handle)
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

            _swIsDone = new UISwitch
            {
                Enabled = false
            };

            BackgroundColor = UIColor.Clear;
            ContentView.AddSubview(_lblName);
            ContentView.AddSubview(_swIsDone);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.DelayBind(
                () =>
                {
                    this.AddBindings(_lblName, Constants.TodoItemNameBindingText);
                    this.AddBindings(_swIsDone, Constants.IsTodoItemDoneBindingText);
                });
        }

        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                _lblName.AtLeftOf(ContentView, Constants.Padding),
                _lblName.AtTopOf(ContentView, Constants.Padding),
                _lblName.AtBottomOf(ContentView, Constants.Padding)
            );

            ContentView.AddConstraints(
                _swIsDone.AtRightOf(ContentView, Constants.Padding),
                _swIsDone.AtTopOf(ContentView, Constants.Padding),
                _lblName.AtBottomOf(ContentView, Constants.Padding)
            );
        }
    }   
}