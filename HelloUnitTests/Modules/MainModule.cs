using Android.Graphics;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Binding;
using MugenMvvmToolkit.Interfaces;
using MugenMvvmToolkit.Interfaces.Models;

namespace HelloUnitTests.Modules
{
    public class MainModule : IModule
    {
        #region Implementation of IModule

        public bool Load(IModuleContext context)
        {
            BindingServiceProvider.ResourceResolver.AddType("Color", typeof(Color));

            return true;
        }

        public void Unload(IModuleContext context)
        {
        }

        public int Priority => ApplicationSettings.ModulePriorityBinding;

        #endregion
    }
}
