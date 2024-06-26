using CineregWPF.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace CineregWPF
{
    /// <summary>
    /// Interaction logic for LoginAuthenticator.xaml
    /// </summary>
    public partial class LoginAuthenticator : Page
    {
        private string Email { get; set; }
        private string Password { get; set; }

        public LoginAuthenticator(string email, string password)
        {
            InitializeComponent();

            Email = email;
            Password = password;
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
                var result = await httpClient.PostAsJsonAsync("https://cinereg-pk.azurewebsites.net/login", new { Email, Password, TwoFactorCode = authenticatorBox.Text }, options);

                if (result.IsSuccessStatusCode)
                {
                    TokenResponse tokenResponse = await result.Content.ReadFromJsonAsync<TokenResponse>();

                    MainWindow main = new(tokenResponse);
                    loginWindow.Close();
                    main.Show();
                    return;
                }

                var error = await result.Content.ReadFromJsonAsync<Error>();

                if (error.Detail == "RequiresTwoFactor") errorText.Text = "Entering your authenticator code is required.";
                else errorText.Text = "Authenticator code was incorrect.";

                return;
            }
            catch (Exception ex)
            {
                errorText.Text = "Unexpected error occurred.";
            }
        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }
    }
}
