using System;
using Core.ViewModels;
using MugenMvvmToolkit;

namespace Core
{
    public class App : MvvmApplication
    {
        #region Overrides of MvvmApplication

        public override Type GetStartViewModelType()
        {
            return typeof(MainViewModel);
        }

        #endregion
    }
}