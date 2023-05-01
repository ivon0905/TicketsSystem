using System.Windows;
using TicketsSystem.Commands;
using TicketsSystem.Models;
using TicketsSystem.Services;

namespace TicketsSystem.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Declarations
        private string username;
        private string password;
        private bool userNotFound;

        private DelegateCommand signInCommand;
        #endregion

        #region Initializations
        public LoginViewModel()
        {

        }
        #endregion

        #region Properties
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public bool UserNotFound
        {
            get { return userNotFound; }
            set
            {
                userNotFound = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                    signInCommand = new DelegateCommand(SignIn);
                return signInCommand;
            }
        }
        #endregion

        #region Methods
        private void SignIn(object o)
        {
            DatabaseService db = new DatabaseService();
            User user = db.FindUser(Username, Password);

            if (user != null)
            {
                MainViewModel vm = new MainViewModel();
                vm.SelectedViewModel = new ReservationViewModel(user);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.DataContext = vm;
            }
            else
            {
                UserNotFound = true;
            }
        }
        #endregion
    }
}
