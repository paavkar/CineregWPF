using CineregWPF.Models;
using Wpf.Ui.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for EditMovieWindow.xaml
    /// </summary>
    public partial class EditMovieWindow : FluentWindow
    {
        public EditMovieWindow(TokenResponse tokenResponse)
        {
            InitializeComponent();

            editMovieFrame.Navigate(new EditMovie(tokenResponse));
        }
    }
}
