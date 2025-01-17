using Microsoft.Maui.Controls;

namespace ReactionsApp
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            usernameLabel.Text = Preferences.Get("username", "");
            emailLabel.Text = Preferences.Get("email", "");

        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("id", "");
            Preferences.Set("username", "");
            Preferences.Set("email", "");
            Preferences.Set("token", "");


            Application.Current.MainPage = new LoginPage();
        }

        public void SetUserDataLabels(string username, string email)
        {
            usernameLabel.Text = username;
            emailLabel.Text = email;
        }
    }
}
