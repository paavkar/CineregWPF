using CineregWPF.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for EditMovie.xaml
    /// </summary>
    public partial class EditMovie : Page
    {
        private string? Id { get; set; }
        private TokenResponse TokenResponse { get; set; }

        public EditMovie(TokenResponse tokenResponse, string? id = null)
        {
            InitializeComponent();

            TokenResponse = tokenResponse;
            if (id != null) Id = id;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Id != null)
            {
                HttpClient httpClient = new();

                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenResponse.AccessToken}");
                var result = await httpClient.GetAsync($"https://cinereg-pk.azurewebsites.net/api/Movies/{Id}");

                if (result.IsSuccessStatusCode)
                {
                    var movie = await result.Content.ReadFromJsonAsync<Movie>();
                }

                else
                {
                    MainWindow mainWindow = (MainWindow)Window.GetWindow(this);

                    mainWindow.mainFrame.Navigate(new MainPage(TokenResponse));
                }
            }
        }
    }
}
