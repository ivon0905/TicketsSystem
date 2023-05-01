using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicketsSystem.Behaviors;

namespace TicketsSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void tbCredentials_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;

            if (sender is TextBox)
            {
                (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            else if (sender is PasswordBox)
            {
                (sender as PasswordBox).GetBindingExpression(PasswordBoxBehavior.PasswordProperty).UpdateSource();
            }

        }

        private void tbCredentials_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            else if (sender is PasswordBox)
            {
                (sender as PasswordBox).GetBindingExpression(PasswordBoxBehavior.PasswordProperty).UpdateSource();
            }
        }
    }
}
