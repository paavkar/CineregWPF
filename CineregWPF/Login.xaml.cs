using Wpf.Ui.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : FluentWindow
    {
        public Login()
        {
            InitializeComponent();

            loginFrame.Navigate(new Uri("LoginForm.xaml", UriKind.Relative));
        }
    }
}
