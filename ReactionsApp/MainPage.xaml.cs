using ReactionsApp.Helpers;
using System.Text.Json;

namespace ReactionsApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnStartingLightsGameButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StartingLightsGamePage());
        }

        private void OnRandomPointsGameButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RandomPointsGamePage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            var isSessionValid = await IsSessionValid();
            if (!isSessionValid)
            {
                await Navigation.PushAsync(new LoginPage());
            }
        }

        private async Task<bool> IsSessionValid()
        {            
            try
            {
                string token = Preferences.Get("token", "");

                if (token == "")
                    return false;

                // Serialize the data to JSON


                // Send a POST request
                var response = await HttpClientWrapper.GetAuthorizedAsync("/api/auth/verify", token);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }

}
