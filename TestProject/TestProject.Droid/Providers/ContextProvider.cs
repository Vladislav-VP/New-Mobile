using Android.Content;

using TestProject.Droid.Providers.Interfaces;

namespace TestProject.Droid.Providers
{
    public class ContextProvider : IContextProvider
    {
        Context _context;

        public ContextProvider(Context context)
        {
            _context = context;
        }

        public Context Context => _context;
    }
}