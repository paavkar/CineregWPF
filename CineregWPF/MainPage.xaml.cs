using CineregWPF.Models;
using System.Windows.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(TokenResponse tokenResponse)
        {
            InitializeComponent();
        }
    }
}
