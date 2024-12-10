using ReactionsApp.Enums;

namespace ReactionsApp;

public partial class StartingLightsGamePage : ContentPage
{
    private const int MILLISECONDS_TO_ILLUMINATE_LIGHT = 1000;
    private const int LIGHTS_COUNT = 5;
    private const int MIN_LIGHTS_EXTINGUISH_MILLISECONDS = 200;
    private const int MAX_LIGHTS_EXTINGUISH_MILLISECONDS = 3000;

    public StartingLightsLaunchMode LaunchMode { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly LaunchTime { get; set; }

    public StartingLightsGamePage()
	{
		InitializeComponent();
		LaunchMode = StartingLightsLaunchMode.ThrottleHit;
	}

	public void OnChangeLaunchModeButtonClicked(object sender, EventArgs e)
	{
		//if ()
		//{

		//}
	}

    public void OnLaunchButtonPressed(object sender, EventArgs e)
    {

    }

    public void OnLaunchButtonReleased(object sender, EventArgs e)
    {

    }

    private void StartGame()
    {
        int extinguishSeconds = Random.Shared.Next(MIN_LIGHTS_EXTINGUISH_MILLISECONDS, MAX_LIGHTS_EXTINGUISH_MILLISECONDS);
        var lights = Lightsboard.Children;
    }
}