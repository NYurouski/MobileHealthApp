using System.Reflection;

namespace oc.Source.Models.ViewModel
{
    class MainViewModel
    {
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
    }
}
