using CineregWPF.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private TokenResponse _tokenResponse;


        JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public MainPage(TokenResponse tokenResponse)
        {
            InitializeComponent();
            _tokenResponse = tokenResponse;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_tokenResponse.AccessToken}");
            var result = await httpClient.GetAsync("https://cinereg-pk.azurewebsites.net/api/Movies");

            if (result.IsSuccessStatusCode)
            {
                var movies = await result.Content.ReadFromJsonAsync<List<Movie>>();

                movieEntries.ItemsSource = movies;
            }
        }

        private async void AddMovieButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
