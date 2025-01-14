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

        string apiUrl = @"https://localhost:7108/api/login";
        var loginData = new { Username = username, Password = password };

        using (var client = HttpClientFactory.Create())
        {
            try
            {
                // Serialize the data to JSON

                string json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a POST request
                var response = await client.PostAsync("/api/auth/login", content);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonSerializer.Deserialize<AuthResponseModel>(responseContent);

                    SessionStorage.Username = responseData.Username;
                    SessionStorage.Token = responseData.Token;

                    errorLabel.IsVisible = false;

                    await DisplayAlert("Success", "Registration successful!", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                    var responseData = JsonSerializer.Deserialize<string>(responseContent);

                    errorLabel.Text = responseContent;
                    errorLabel.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = "An error occured. Please try again.";
                errorLabel.IsVisible = true;
            }
        }
    }

    // Handle Register Link Click
    private void OnRegisterTapped(object sender, EventArgs e)
    {
        // Navigate to the Registration Page (you can create one)
        Navigation.PushAsync(new RegisterPage());
    }
}