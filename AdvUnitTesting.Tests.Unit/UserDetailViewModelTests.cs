using NUnit.Framework;
using System;
using AdvUnitTesting.Core;
using Moq;
using AutoFixture;
using Should;

namespace AdvUnitTesting.Tests.Unit
{
    [TestFixture()]
    public class UserDetailViewModelTests
    {
        private IFixture _fixture;
        private Mock<IRemoteWebRepository> _remoteWebRepository;
        private Mock<ILocalDbRepository> _localDbRepository;
        private Mock<IAuthenticationService> _authenticationService;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _remoteWebRepository = new Mock<IRemoteWebRepository>();
            _localDbRepository = new Mock<ILocalDbRepository>();
            _authenticationService = new Mock<IAuthenticationService>();
        }


        [Test]
        public void IsLoggedIn_should_be_false_to_start()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.IsLoggedIn.ShouldBeFalse();
        }

        [Test]
        public void IsChanged_should_be_false_to_start()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.IsChanged.ShouldBeFalse();
        }

        [Test]
        public void IsFavorite_should_be_false_to_start()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.IsFavorite.ShouldBeFalse();
        }

        [Test]
        public void Setting_first_name_should_set_full_name()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.FullName.ShouldEqual(string.Empty);
            var firstName = _fixture.Create<string>();
            vm.FirstName = firstName;
            vm.FullName.ShouldEqual(firstName);
        }

        [Test]
        public void Setting_last_name_should_set_full_name()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.FullName.ShouldEqual(string.Empty);
            var lastName = _fixture.Create<string>();
            vm.LastName = lastName;
            vm.FullName.ShouldEqual(lastName);
        }

        [Test]
        public void Full_name_should_concatenate_first_and_last_names()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.FullName.ShouldEqual(string.Empty);

            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();

            vm.FirstName = firstName;
            vm.LastName = lastName;

            vm.FullName.ShouldEqual(firstName + " " + lastName);
        }

        [Test]
        public void Load_should_set_first_name()
        {
            var userId = _fixture.Create<int>();

            var user = _fixture.Create<UserModel>();
            _remoteWebRepository.Setup(x => x.LoadUser(userId)).ReturnsAsync(user);

            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);

            vm.LoadCommand.Execute(userId);
            vm.FirstName.ShouldEqual(user.FirstName);
        }

        [Test]
        public void Load_should_set_last_name()
        {
            var userId = _fixture.Create<int>();

            var user = _fixture.Create<UserModel>();
            _remoteWebRepository.Setup(x => x.LoadUser(userId)).ReturnsAsync(user);

            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);

            vm.LoadCommand.Execute(userId);
            vm.LastName.ShouldEqual(user.LastName);
        }

        [Test]
        public void Save_should_save_user_to_db()
        {
            
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.SaveCommand.Execute(null);
            _localDbRepository.Verify(x => x.Save(It.IsAny<UserModel>()), Times.Once);
        }

        [Test]
        public void Can_save_when_user_changed()
        {
            var vm = new UserDetailViewModel(
                _remoteWebRepository.Object, 
                _localDbRepository.Object,
                _authenticationService.Object)
            {
                FirstName = _fixture.Create<string>()
            };
            vm.SaveCommand.CanExecute(null).ShouldBeTrue();
        }

        [Test]
        public void Can_not_save_when_user_not_changed()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.SaveCommand.CanExecute(null).ShouldBeFalse();
        }

        [Test]
        public void IsLoggedIn_should_be_true_when_logging_in()
        {
            
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();

            _authenticationService.Setup(x => x.Login(userName, password)).ReturnsAsync(true);
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.UserName = userName;
            vm.Password = password;

            vm.LoginCommand.Execute(null);
            vm.IsLoggedIn.ShouldBeTrue();
        }

        [Test]
        public void Can_login_when_userName_and_password_set()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.UserName = _fixture.Create<string>();
            vm.Password = _fixture.Create<string>();
            vm.LoginCommand.CanExecute(null).ShouldBeTrue();
        }

        [Test]
        public void Can_not_login_when_userName_or_password_not_set()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.LoginCommand.CanExecute(null).ShouldBeFalse();
        }

        [Test]
        public void Can_make_favorite_when_user_loaded()
        {
            var userId = _fixture.Create<int>();
            var user = _fixture.Create<UserModel>();

            _remoteWebRepository.Setup(x => x.LoadUser(userId)).ReturnsAsync(user);

            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.LoadCommand.Execute(userId);
            vm.MakeFavoriteCommand.CanExecute(null).ShouldBeTrue();
        }

        [Test]
        public void Can_not_make_favorite_when_user_not_loaded()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            vm.MakeFavoriteCommand.CanExecute(null).ShouldBeFalse();
        }

        [Test]
        public void IsFavorite_should_be_true_when_make_favorite()
        {
            var vm = new UserDetailViewModel(_remoteWebRepository.Object, _localDbRepository.Object, _authenticationService.Object);
            var userId = _fixture.Create<int>();
            vm.LoadCommand.Execute(userId);
            vm.MakeFavoriteCommand.Execute(null);
            vm.IsFavorite.ShouldBeTrue();
        }
    }
}