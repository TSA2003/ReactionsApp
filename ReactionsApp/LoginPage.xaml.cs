namespace ReactionsApp;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    // Handle Login Button Click
    private void OnLoginClicked(object sender, EventArgs e)
    {        

        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        // Example mock login validation (replace with your actual logic)
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorLabel.Text = "Please enter both username and password.";
            errorLabel.IsVisible = true;
        }
        else if (username == "admin" && password == "password") // Replace with your actual validation logic
        {
            // Mock login successful
            errorLabel.IsVisible = false;
            Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            // Navigate to the next page (e.g., Home Page)
            //Navigation.PushAsync(new HomePage());
        }
        else
        {
            // Mock login failed
            errorLabel.Text = "Invalid username or password.";
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