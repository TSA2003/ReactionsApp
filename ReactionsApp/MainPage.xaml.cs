namespace ReactionsApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            Navigation.PushAsync(new LoginPage());
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        }

        private void OnStartingLightsGameButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StartingLightsGamePage());
        }

        private void OnRandomPointsGameButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RandomPointsGamePage());
        }
    }

}
