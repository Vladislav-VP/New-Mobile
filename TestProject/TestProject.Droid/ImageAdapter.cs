using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace TestProject.Droid
{
    public class ImageAdapter : BaseAdapter
    {
        private Context _context;

        private int[] _images =
        {
            Resource.Drawable.car1,
            Resource.Drawable.car2,
            Resource.Drawable.car3,
            Resource.Drawable.car4,
            Resource.Drawable.car5,
            Resource.Drawable.car6,
            Resource.Drawable.car7,
            Resource.Drawable.car8,
            Resource.Drawable.car9,
            Resource.Drawable.car10
        };

        public ImageAdapter(Context context)
        {
            _context = context;
        }

        public override int Count
        {
            get
            {
                return _images.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView image = new ImageView(_context);
            image.SetImageResource(_images[position]);
            image.SetScaleType(ImageView.ScaleType.FitXy);
            image.LayoutParameters = new Gallery.LayoutParams(200, 100);
            return image;
        }
    }
}