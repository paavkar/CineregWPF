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
                    EditMovieWindow editMovieWindow = (EditMovieWindow)Window.GetWindow(this);

                    editMovieWindow.Close();
                }
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Movie movie = new()
            {
                Name = movieTitleBox.Text,
                Genre = movieGenreBox.Text,
                Director = movieDirectorBox.Text,
                ReleaseYear = (int)movieReleaseYearBox.Value,
                WatchedYear = (int)movieWatchedYearBox.Value,
                ViewingForm = movieFormatBox.Text,
                Review = movieReviewBox.SelectedValue.ToString()
            };

            HttpClient httpClient = new();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenResponse.AccessToken}");
            var result = await httpClient.PostAsJsonAsync($"https://cinereg-pk.azurewebsites.net/api/Movies/", movie);

            if (result.IsSuccessStatusCode)
            {
                EditMovieWindow editMovieWindow = (EditMovieWindow)Window.GetWindow(this);

                editMovieWindow.Close();
            }
        }
    }
}
