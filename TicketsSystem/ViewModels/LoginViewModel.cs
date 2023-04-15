using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsSystem.Commands;

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
            //User user = UserData.IsUserPassCorrect(username, password);
            ////Проверяваме дали потребителят съществува и дали е ученик, за да заредим view-то с неговите данни
            //if (user != null && user.Role == 4)
            //{
            //    MainViewModel vm = new MainViewModel();
            //    vm.SelectedViewModel = new EditStudentViewModel(user);
            //    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            //    mainWindow.DataContext = vm;
            //}
            ////Проверяваме дали потребителят е преподавател и зареждаме данните за всички студенти
            //else if (user != null && user.Role == 3)
            //{
            //    MainViewModel vm = new MainViewModel();
            //    vm.SelectedViewModel = new AllStudentsViewModel();
            //    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            //    mainWindow.DataContext = vm;
            //}
            //else
            //{
            //    UserNotFound = true;
            //}
        }
        #endregion
    }
}
