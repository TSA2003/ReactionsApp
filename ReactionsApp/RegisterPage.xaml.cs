using System.Net;
using System.Text.Json;
using System.Text;
using ReactionsApp.Models;
using ReactionsApp.Helpers;

namespace ReactionsApp;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
    }

    // Handle Register Button Click
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string email = emailEntry.Text;
        string password = passwordEntry.Text;
        string confirmPassword = confirmPasswordEntry.Text;

        // Validate user input
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            errorLabel.Text = "Please fill out all fields.";
            errorLabel.IsVisible = true;
            return;
        }
        else if (password != confirmPassword)
        {
            errorLabel.Text = "Passwords do not match.";
            errorLabel.IsVisible = true;
            return;
        }
        else if (!IsValidEmail(email))
        {
            errorLabel.Text = "Please enter a valid email address.";
            errorLabel.IsVisible = true;
            return;
        }

        
        var loginData = new { Username = username, Email = email, Password = password };

        try
        {
            // Serialize the data to JSON

            string json = JsonSerializer.Serialize(loginData);
            var body = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a POST request
            var response = await HttpClientWrapper.PostAsync("/api/auth/register", body);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonSerializer.Deserialize<AuthResponseModel>(responseContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                Preferences.Set("id", responseData.User.Id.ToString());
                Preferences.Set("username", responseData.User.Username);
                Preferences.Set("email", responseData.User.Email);
                Preferences.Set("token", responseData.Token);

                Application.Current.MainPage = new AppShell();

                (Shell.Current as AppShell).SetUserDataLabels(responseData.User.Username, responseData.User.Email);

                errorLabel.IsVisible = false;

                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else
            {
                var responseData = responseContent;

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
    

    // Validate Email Format (simple example)
    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    // Handle Login Link Tap
    private void OnLoginTapped(object sender, EventArgs e)
    {
        // Navigate to Login Page
        //Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        Navigation.PopAsync();
    }
}