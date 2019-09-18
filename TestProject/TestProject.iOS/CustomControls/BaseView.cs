using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace TestProject.iOS.CustomControls
{
    public abstract class BaseView : UIView
    {
        private bool _didSetupConstraints;

        public BaseView()
        {
            RunLifecycle();
        }

        public override sealed void UpdateConstraints()
        {
            if (!_didSetupConstraints)
            {
                CreateConstraints();

                _didSetupConstraints = true;
            }
            base.UpdateConstraints();
        }

        protected void RunLifecycle()
        {
            CreateViews();

            SetNeedsUpdateConstraints();
        }

        protected abstract void CreateViews();

        protected abstract void CreateConstraints();
    }
}