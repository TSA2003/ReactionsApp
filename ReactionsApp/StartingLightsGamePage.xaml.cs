using ReactionsApp.Enums;
using ReactionsApp.Helpers;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace ReactionsApp;

public partial class StartingLightsGamePage : ContentPage
{
    private const int MILLISECONDS_TO_ILLUMINATE_LIGHT = 1000;
    private const int LIGHTS_COUNT = 5;
    private const int MIN_MILLISECONDS_TO_EXTINGUISH_LIGHTS = 200;
    private const int MAX_MILLISECONDS_TO_EXTINGUISH_LIGHTS = 3000;

    public StartingLightsLaunchMode LaunchMode { get; set; }
    public IDispatcherTimer LightIlluminateTimer { get; set; }
    public IDispatcherTimer LightsExtinguishTimer { get; set; }
    public Stopwatch ReactionStopwatch { get; set; }
    public bool IsGameRunning { get; set; }

    public StartingLightsGamePage()
	{
		InitializeComponent();
		LaunchMode = StartingLightsLaunchMode.ThrottleHit;
        ChangeLaunchModeButton.Text = LaunchMode.ToString();
        LightIlluminateTimer = Dispatcher.CreateTimer();
        LightsExtinguishTimer = Dispatcher.CreateTimer();
        ReactionStopwatch = new Stopwatch();
	}

	public void OnChangeLaunchModeButtonClicked(object sender, EventArgs e)
	{
        if (IsGameRunning)
        {
            LightIlluminateTimer.Stop();
            LightsExtinguishTimer.Stop();
            StopGame();
        }

        if (LaunchMode == StartingLightsLaunchMode.ThrottleHit)
        {
            LaunchMode = StartingLightsLaunchMode.ClutchRelease;
        }
        else if (LaunchMode == StartingLightsLaunchMode.ClutchRelease)
        {
            LaunchMode = StartingLightsLaunchMode.ThrottleHit;
        }

        ChangeLaunchModeButton.Text = LaunchMode.ToString();
    }

    public void OnLaunchButtonPressed(object sender, EventArgs e)
    {
        if (IsGameRunning && LaunchMode == StartingLightsLaunchMode.ThrottleHit)
        {
            Launch();
        }
        else if (LaunchMode == StartingLightsLaunchMode.ClutchRelease)
        {
            ResetGame();
            InitializeGame();
        }
    }

    public void OnLaunchButtonReleased(object sender, EventArgs e)
    {
        if (IsGameRunning && LaunchMode == StartingLightsLaunchMode.ThrottleHit)
        {
            StopGame();
        }
        else if (!IsGameRunning && LaunchMode == StartingLightsLaunchMode.ThrottleHit)
        {
            ResetGame();
            InitializeGame();
        }
        else if (IsGameRunning && LaunchMode == StartingLightsLaunchMode.ClutchRelease)
        {
            Launch();
        }
    }

    private void InitializeGame()
    {
        int extinguishSeconds = Random.Shared.Next(MIN_MILLISECONDS_TO_EXTINGUISH_LIGHTS, MAX_MILLISECONDS_TO_EXTINGUISH_LIGHTS);
        var lights = Lightsboard.Children;
        int currentLightIndex = 1;

        var tempLight = lights[0] as Border;
        tempLight.BackgroundColor = Color.FromRgb(255, 0, 0);

        LightIlluminateTimer = Dispatcher.CreateTimer();
        LightIlluminateTimer.Interval = TimeSpan.FromMilliseconds(MILLISECONDS_TO_ILLUMINATE_LIGHT);
        LightIlluminateTimer.Tick += (s, e) =>
        {
            if (currentLightIndex == lights.Count() - 1)
            { 
                LightIlluminateTimer.Stop();
            }

            var tempLight = lights[currentLightIndex] as Border;
            tempLight.BackgroundColor = Color.FromRgb(255, 0, 0);
            currentLightIndex++;
        };
        LightIlluminateTimer.Start();

        LightsExtinguishTimer = Dispatcher.CreateTimer();
        LightsExtinguishTimer.Interval = TimeSpan.FromMilliseconds(MILLISECONDS_TO_ILLUMINATE_LIGHT * LIGHTS_COUNT + extinguishSeconds);
        LightsExtinguishTimer.IsRepeating = false;
        LightsExtinguishTimer.Tick += (s, e) =>
        {
            foreach (var temp in lights)
            {
                var tempLight = temp as Border;
                tempLight.BackgroundColor = Color.FromRgb(0, 0, 0);
            }
            ReactionStopwatch = Stopwatch.StartNew();
        };
        LightsExtinguishTimer.Start();

        IsGameRunning = true;
        GameStatusLabel.Text = "Light sequence begins...";
    }

    private void ResetGame()
    { 
        ReactionStopwatch.Reset();
        LightIlluminateTimer.Stop();
        LightsExtinguishTimer.Stop();
        var lights = Lightsboard.Children;
        foreach (var temp in lights)
        {
            var tempLight = temp as Border;
            tempLight.BackgroundColor = Color.FromRgb(0, 0, 0);
        }
    }

    private void StopGame()
    {
        IsGameRunning = false;
    }

    private async void Launch()
    {
        LightIlluminateTimer.Stop();
        LightsExtinguishTimer.Stop();

        if (!ReactionStopwatch.IsRunning)
        {
            GameStatusLabel.Text = "False start!";            
        }
        else
        {
            ReactionStopwatch.Stop();
            var time = ReactionStopwatch.Elapsed;
            GameStatusLabel.Text = $"Reaction time: {time:ss}.{time:fff} seconds";

            await SaveResult(time);      
        }
    }

    private async Task SaveResult(TimeSpan result)
    {
        try
        {
            var gameResultData = new { ReactionTime = result, GameMode = LaunchMode, PlayerId = Preferences.Get("id", "") };
            string json = JsonSerializer.Serialize(gameResultData);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClientWrapper.PostAuthorizedAsync("/api/startinglightsgameresult", body, Preferences.Get("token", ""));

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", "Could not save game result!", "OK");
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "An error occured!", "OK");
        }
    }
}