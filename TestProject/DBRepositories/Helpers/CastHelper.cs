using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;

namespace TestProject.Services.Helpers
{
    public static class CastHelper<T>
    {
        public static MvxObservableCollection<T> ToMvxObservableCollection(IEnumerable<T> initialCollection)
        {
            MvxObservableCollection<T> resultCollection = new MvxObservableCollection<T>();
            foreach (var item in initialCollection)
            {
                resultCollection.Add(item);
            }
            return resultCollection;
        }
    }
}
