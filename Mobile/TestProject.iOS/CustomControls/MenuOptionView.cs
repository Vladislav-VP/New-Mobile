using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace TestProject.iOS.CustomControls
{
    public class MenuOptionView : BaseView
    {
        public UILabel Label { get; set; }

        public UIView Line { get; set; }

        public MenuOptionView()
        {
        }

        protected override void CreateViews()
        {
            Label = new UILabel
            {
                TextColor = UIColor.Black,
                Font = UIFont.SystemFontOfSize(Constants.ListFontSize, UIFontWeight.Bold)
            };

            Line = new UIView { BackgroundColor = UIColor.Black };

            AddSubviews(Label, Line);
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
        }

        protected override void CreateConstraints()
        {
            this.AddConstraints(
                Label.AtTopOf(this, Constants.Padding),
                Label.AtLeftOf(this, Constants.Padding),

                Line.Below(Label, Constants.Padding),
                Line.AtLeftOf(this, Constants.Padding),
                Line.AtRightOf(this, Constants.Padding),
                Line.AtBottomOf(this),
                Line.Height().EqualTo(Constants.LineHeight)
            );
        }
    }
}