using ReactionsApp.Helpers;
using ReactionsApp.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ReactionsApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
    }

    // Handle Login Button Click
    private async void OnLoginClicked(object sender, EventArgs e)
    {        

        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        // Example mock login validation (replace with your actual logic)
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorLabel.Text = "Please enter both username and password.";
            errorLabel.IsVisible = true;
            return;
        }

        var loginData = new { Username = username, Password = password };        
        try
        {
            // Serialize the data to JSON

            string json = JsonSerializer.Serialize(loginData);
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a POST request
            var response = await HttpClientWrapper.PostAsync("/api/auth/login", body);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonSerializer.Deserialize<AuthResponseModel>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                Preferences.Set("id", responseData.User.Id.ToString());
                Preferences.Set("username", responseData.User.Username);
                Preferences.Set("email", responseData.User.Email);
                Preferences.Set("token", responseData.Token);

                Application.Current.MainPage = new AppShell();

                (Shell.Current as AppShell).SetUserDataLabels(responseData.User.Username, responseData.User.Email);

                errorLabel.IsVisible = false;

                await Shell.Current.GoToAsync($"//{ nameof(MainPage) }?clear=true");
            }
            else
            {
                var responseData = content;

                errorLabel.Text = content;
                errorLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = "An error occured. Please try again.";
            errorLabel.IsVisible = true;
        }
        
    }

    // Handle Register Link Click
    private void OnRegisterTapped(object sender, EventArgs e)
    {
        // Navigate to the Registration Page (you can create one)
        Navigation.PushAsync(new RegisterPage());
    }
}