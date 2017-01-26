using Core;
using HelloUnitTests;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Android.Attributes;
using MugenMvvmToolkit.Android.Infrastructure;
using MugenMvvmToolkit.Interfaces;

[assembly: Bootstrapper(typeof(Setup))]

namespace HelloUnitTests
{
    public class Setup : AndroidBootstrapperBase
    {
        #region Overrides of AndroidBootstrapperBase

        protected override IIocContainer CreateIocContainer()
        {
            return new AutofacContainer();
        }

        protected override IMvvmApplication CreateApplication()
        {
            return new App();
        }

        #endregion
    }
}
