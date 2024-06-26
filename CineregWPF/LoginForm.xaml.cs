using CineregWPF.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Page
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            HttpClient httpClient = new();
            Login loginWindow = (Login)Window.GetWindow(this);

            try
            {
                var result = await httpClient.PostAsJsonAsync("https://cinereg-pk.azurewebsites.net/login", new { Email = emailBox.Text, Password = passwordBox.Password }, options);

                if (result.IsSuccessStatusCode)
                {
                    TokenResponse tokenResponse = await result.Content.ReadFromJsonAsync<TokenResponse>();

                    MainWindow main = new(tokenResponse);
                    loginWindow.Close();
                    main.Show();
                    return;
                }

                var error = await result.Content.ReadFromJsonAsync<Error>();

                if (error.Detail == "RequiresTwoFactor") loginWindow.loginFrame.Navigate(new LoginAuthenticator(emailBox.Text, passwordBox.Password));

                else errorText.Text = "Incorrect email or password.";

                return;
            }
            catch (Exception ex)
            {
                errorText.Text = "Unexpected error occurred.";
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            string url = "https://cinereg-pk.azurewebsites.net/Account/Register";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
}
