
using System;
using System.Drawing;

using Foundation;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using TestProject.Core.ViewModels;
using UIKit;

namespace TestProject.iOS.Views
{
    [MvxChildPresentation]
    public partial class LoginViewController : MvxViewController<LoginViewModel>
    {
        public LoginViewController()
        {

        }

        public LoginViewController(IntPtr handle) : base(handle)
        {
            try
            {
                var p = 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            try
            {
                base.ViewDidLoad();
            }
            catch (Exception ex)
            {

                throw;
            }
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            try
            {
                base.ViewWillAppear(animated);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            try
            {
                base.ViewDidAppear(animated);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}