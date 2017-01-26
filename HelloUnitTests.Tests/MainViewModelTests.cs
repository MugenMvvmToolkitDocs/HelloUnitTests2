using System;
using Core.Models;
using Core.ViewModels;
using Moq;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Infrastructure;
using MugenMvvmToolkit.Interfaces;
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Models;
using NUnit.Framework;

namespace HelloUnitTests.Tests
{
    [TestFixture]
    public class MainViewModelTests : UnitTestBase
    {
        [SetUp]
        public void SetUp()
        {
            _viewModelPresenterMock = new Mock<IViewModelPresenter>();

            _serializer = new Serializer(AppDomain.CurrentDomain.GetAssemblies());

            var container = new AutofacContainer();
            container.BindToConstant(_viewModelPresenterMock.Object);

            Initialize(container, new DefaultUnitTestModule());

            ApplicationSettings.CommandExecutionMode = CommandExecutionMode.None;
        }

        private Mock<IViewModelPresenter> _viewModelPresenterMock;
        private ISerializer _serializer;

        private TState UpdateState<TState>(TState state)
            where TState : IDataContext
        {
            var stream = _serializer.Serialize(state);
            stream.Position = 0;
            return (TState)_serializer.Deserialize(stream);
        }

        [Test]
        public void AddUserCmdCanBeExecutedAlways()
        {
            var viewModel = GetViewModel<MainViewModel>();
            Assert.IsTrue(viewModel.AddUserCommand.CanExecute(null));

            var user = new User
            {
                Firstname = "TestFirstname",
                Lastname = "TestLastname"
            };
            viewModel.UserGridViewModel.ItemsSource.Add(user);
            viewModel.UserGridViewModel.SelectedItem = user;

            Assert.IsTrue(viewModel.AddUserCommand.CanExecute(null));
        }

        [Test]
        public void DeleteCmdCanBeExecutedSelectedItemNotNull()
        {
            var viewModel = GetViewModel<MainViewModel>();

            var user = new User
            {
                Firstname = "TestFirstname",
                Lastname = "TestLastname"
            };
            viewModel.UserGridViewModel.ItemsSource.Add(user);
            viewModel.UserGridViewModel.SelectedItem = user;

            Assert.IsTrue(viewModel.DeleteUserCommand.CanExecute(null));
        }

        [Test]
        public void DeleteCmdCannotBeExecutedSelectedItemNull()
        {
            var viewModel = GetViewModel<MainViewModel>();

            Assert.IsNull(viewModel.UserGridViewModel.SelectedItem);
            Assert.IsFalse(viewModel.DeleteUserCommand.CanExecute(null));
        }

        [Test]
        public void VmShouldInitializeCommandsAndUserGridViewModel()
        {
            var viewModel = GetViewModel<MainViewModel>();
            Assert.IsNotNull(viewModel.AddUserCommand, "AddUserCommand is null");
            Assert.IsNotNull(viewModel.DeleteUserCommand, "DeleteUserCommand is null");

            Assert.IsNotNull(viewModel.UserGridViewModel, "UserGridViewModel is null");
        }

        [Test]
        public void VmShouldSaveAndRestoreState()
        {
            var model = new User
            {
                Firstname = "TestFirstname",
                Lastname = "TestLastname"
            };

            var viewModel = GetViewModel<MainViewModel>();
            viewModel.InitializeEntity(model, false);

            var state = new DataContext();
            viewModel.SaveState(state);
            state = UpdateState(state);

            viewModel = GetViewModel<MainViewModel>();
            viewModel.LoadState(state);

            Assert.IsTrue(viewModel.IsEntityInitialized);
            Assert.IsFalse(viewModel.IsNewRecord);
            Assert.AreEqual(viewModel.Lastname, model.Lastname);
            Assert.AreEqual(viewModel.Firstname, model.Firstname);
        }
    }
}
