using CineregWPF.Models;
using Wpf.Ui.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        TokenResponse _tokenResponse;

        public MainWindow(TokenResponse tokenResponse)
        {
            InitializeComponent();

            _tokenResponse = tokenResponse;
            mainFrame.Navigate(new MainPage(tokenResponse));
        }
    }
}