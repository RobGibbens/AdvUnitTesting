using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdvUnitTesting.Core
{
    public class UserDetailViewModel
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}".Trim(); }
        }

        public bool IsLoggedIn { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsChanged { get; set; }
        public bool IsFavorite { get; private set; }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand MakeFavoriteCommand { get; }
        #endregion

        private UserModel _user;

        public UserDetailViewModel()
        {
            SaveCommand = new DelegateCommand(async () => await Save(), CanSave);
            LoadCommand = new DelegateCommand<int>(async (userId) => await Load(userId));
            LoginCommand = new DelegateCommand(async () => await Login(), CanLogin);
            MakeFavoriteCommand = new DelegateCommand(MakeFavorite, CanMakeFavorite);
        }

        private async Task Load(int id)
        {
            var repository = new RemoteWebRepository();
            _user = await repository.LoadUser(id);
            this.FirstName = _user.FirstName;
            this.LastName = _user.LastName;
        }

        private async Task Save()
        {
            _user.FirstName = this.FirstName;
            _user.LastName = this.LastName;

            var db = new LocalDbRepository();
            await db.Save(_user);
        }

        private bool CanSave()
        {
            return this.IsChanged;
        }

        private void MakeFavorite()
        {
            this.IsFavorite = true;
        }

        private bool CanMakeFavorite()
        {
            return _user != null;
        }

        private async Task Login()
        {
            var authService = new AuthenticationService(this.UserName, this.Password);
            this.IsLoggedIn = await authService.Login();
        }

        private bool CanLogin()
        {
            return (!string.IsNullOrWhiteSpace(this.UserName) && !string.IsNullOrWhiteSpace(this.Password));
        }
    }
}
