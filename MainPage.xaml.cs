namespace ReactionsApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnStartingLightsGameButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StartingLightsGamePage());
        }
    }

}
