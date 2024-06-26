using CineregWPF.Models;
using System.Windows;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(TokenResponse tokenResponse)
        {
            InitializeComponent();

            mainFrame.Navigate(new MainPage(tokenResponse));
        }
    }
}