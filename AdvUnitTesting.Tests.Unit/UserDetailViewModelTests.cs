using NUnit.Framework;
using System;
using AdvUnitTesting.Core;
using Ploeh.AutoFixture;
using Should;

namespace AdvUnitTesting.Tests.Unit
{
    [TestFixture()]
    public class UserDetailViewModelTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }


        [Test]
        public void IsLoggedIn_should_be_false_to_start()
        {
            var vm = new UserDetailViewModel();
            vm.IsLoggedIn.ShouldBeFalse();
        }

        [Test]
        public void IsChanged_should_be_false_to_start()
        {
            var vm = new UserDetailViewModel();
            vm.IsChanged.ShouldBeFalse();
        }

        [Test]
        public void IsFavorite_should_be_false_to_start()
        {
            var vm = new UserDetailViewModel();
            vm.IsFavorite.ShouldBeFalse();
        }

        [Test]
        public void Setting_first_name_should_set_full_name()
        {
            var vm = new UserDetailViewModel();
            vm.FullName.ShouldEqual(string.Empty);
            var firstName = _fixture.Create<string>();
            vm.FirstName = firstName;
            vm.FullName.ShouldEqual(firstName);
        }

        [Test]
        public void Setting_last_name_should_set_full_name()
        {
            var vm = new UserDetailViewModel();
            vm.FullName.ShouldEqual(string.Empty);
            var lastName = _fixture.Create<string>();
            vm.LastName = lastName;
            vm.FullName.ShouldEqual(lastName);
        }

        [Test]
        public void Full_name_should_concatenate_first_and_last_names()
        {
            var vm = new UserDetailViewModel();
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
            var vm = new UserDetailViewModel();

            var userId = _fixture.Create<int>();
            var firstName = _fixture.Create<string>();
            vm.LoadCommand.Execute(userId);
            vm.FirstName.ShouldEqual(firstName);
        }

        [Test]
        public void Load_should_set_last_name()
        {
            var vm = new UserDetailViewModel();

            var userId = _fixture.Create<int>();
            var lastName = _fixture.Create<string>();
            vm.LoadCommand.Execute(userId);
            vm.LastName.ShouldEqual(lastName);
        }

        [Test]
        public void Save_should_save_user_to_db()
        {
            Assert.Fail();
        }

        [Test]
        public void Can_save_when_user_changed()
        {
            var vm = new UserDetailViewModel
            {
                FirstName = _fixture.Create<string>()
            };
            vm.SaveCommand.CanExecute(null).ShouldBeTrue();
        }

        [Test]
        public void Can_not_save_when_user_not_changed()
        {
            var vm = new UserDetailViewModel();
            vm.SaveCommand.CanExecute(null).ShouldBeFalse();
        }

        [Test]
        public void IsLoggedIn_should_be_true_when_logging_in()
        {
            var vm = new UserDetailViewModel
            {
                UserName = _fixture.Create<string>(),
                Password = _fixture.Create<string>()
            };
            vm.LoginCommand.Execute(null);
            vm.IsLoggedIn.ShouldBeTrue();
        }

        [Test]
        public void Can_login_when_userName_and_password_set()
        {
            var vm = new UserDetailViewModel
            {
                UserName = _fixture.Create<string>(),
                Password = _fixture.Create<string>()
            };
            vm.LoginCommand.CanExecute(null).ShouldBeTrue();
        }

        [Test]
        public void Can_not_login_when_userName_or_password_not_set()
        {
            var vm = new UserDetailViewModel();
            vm.LoginCommand.CanExecute(null).ShouldBeFalse();
        }

        [Test]
        public void Can_make_favorite_when_user_loaded()
        {
            var vm = new UserDetailViewModel();
            var userId = _fixture.Create<int>();
            vm.LoadCommand.Execute(userId);
            vm.MakeFavoriteCommand.CanExecute(null).ShouldBeTrue();
        }

        [Test]
        public void Can_not_make_favorite_when_user_not_loaded()
        {
            var vm = new UserDetailViewModel();
            vm.MakeFavoriteCommand.CanExecute(null).ShouldBeFalse();
        }

        [Test]
        public void IsFavorite_should_be_true_when_make_favorite()
        {
            var vm = new UserDetailViewModel();
            var userId = _fixture.Create<int>();
            vm.LoadCommand.Execute(userId);
            vm.MakeFavoriteCommand.Execute(null);
            vm.IsFavorite.ShouldBeTrue();
        }
    }
}