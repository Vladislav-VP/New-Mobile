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

        private const int _width = 100;
        private const int _height = 100;
        private const int _left = 8;
        private const int _right = 8;
        private const int _top = 8;
        private const int _bottom = 8;

        private int[] _cars =
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
                return _cars.Length;
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
            ImageView imageView;
            if (convertView == null)
            {
                imageView = new ImageView(_context);
                imageView.LayoutParameters = new GridView.LayoutParams(_width, _height);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(_left, _top, _right, _bottom);
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            imageView.SetImageResource(_cars[position]);
            return imageView;
        }
    }
}