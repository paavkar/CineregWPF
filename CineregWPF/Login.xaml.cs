using System.Windows;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            loginFrame.Navigate(new Uri("LoginForm.xaml", UriKind.Relative));
        }
    }
}
