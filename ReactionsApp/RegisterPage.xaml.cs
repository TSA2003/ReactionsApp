namespace ReactionsApp;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    // Handle Register Button Click
    private void OnRegisterClicked(object sender, EventArgs e)
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
        }
        else if (password != confirmPassword)
        {
            errorLabel.Text = "Passwords do not match.";
            errorLabel.IsVisible = true;
        }
        else if (!IsValidEmail(email))
        {
            errorLabel.Text = "Please enter a valid email address.";
            errorLabel.IsVisible = true;
        }
        else
        {
            // Simulate a successful registration
            errorLabel.IsVisible = false;

            // You would typically send the data to your API for actual registration
            DisplayAlert("Success", "Registration successful!", "OK");

            // Navigate to Login Page or Home Page
            Navigation.PushAsync(new LoginPage());
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