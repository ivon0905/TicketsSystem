namespace TicketsSystem.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties
        private BaseViewModel selectedViewModel = new LoginViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged(nameof(selectedViewModel));
            }
        }

        #endregion
    }
}
