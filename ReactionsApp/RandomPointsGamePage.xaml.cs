using Microsoft.Maui.Layouts;
using System.Text.Json;
using System.Text;
using ReactionsApp.Helpers;

namespace ReactionsApp;

public partial class RandomPointsGamePage : ContentPage
{
	private const int GAME_DURATION_IN_SECONDS = 10;
    private const int INITIAL_SCORE = 0;
    private const int TICK_SECONDS = 1;
    private readonly Color FILL_COLOR = Color.FromRgb(0, 0, 255);
    private readonly Color CLICKED_FILL_COLOR = Color.FromRgb(0, 255, 0);
    private const double RADIUS = 20;

    public Point CurrentCircleCenter { get; set; }
    public bool IsCurrentCircleSelected { get; set; }
    public IDispatcherTimer GameTimer { get; set; }
    public int Score { get; set; }
    public int RemainingSeconds { get; set; }

    public RandomPointsGamePage()
	{
		InitializeComponent();        
		StartGame();
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GameTimer.Stop();
    }

    private void StartGame()
	{
        Score = INITIAL_SCORE;
        RemainingSeconds = GAME_DURATION_IN_SECONDS;
        ScoreLabel.Text = $"Score: {Score.ToString()}";
        RemainingSecondsLabel.Text = $"Time left: {RemainingSeconds.ToString()}";
        GameTimer = Dispatcher.CreateTimer();
		GameTimer.Interval = TimeSpan.FromSeconds(TICK_SECONDS);
		GameTimer.Tick += async (s, e) =>
		{
            if (RemainingSeconds == 0)
            {
                GameTimer.Stop();
                await SaveResult(Score);
                GameField.RemoveAt(GameField.Count - 1);
                return;
            }
            RemainingSeconds--;
            RemainingSecondsLabel.Text = $"Time left: {RemainingSeconds.ToString()}";
        };
		GameTimer.Start();

        GenerateButton();
    }

    public void GenerateButton()
    {
        var button = new Button
        {
            BackgroundColor = Colors.Blue,
            TextColor = Colors.White,
            CornerRadius = 50,
            WidthRequest = 10,
            HeightRequest = 10
        };

        button.Pressed += (s, e) =>
        {
            Score++;
            ScoreLabel.Text = $"Score: {Score.ToString()}";
            button.BackgroundColor = Colors.Green;
            GameField.Remove(button);
            GenerateButton();            
        };
        button.Released += (s, e) =>
        {
            
        };

        double min = 0.1;
        double max = 0.95;

        double buttonX = Random.Shared.NextDouble() * (max - min) + min;
        double buttonY = Random.Shared.NextDouble() * (max - min) + min;

        GameField.SetLayoutBounds(button, new Rect(buttonX,
        buttonY, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

        AbsoluteLayout.SetLayoutFlags(button,
            AbsoluteLayoutFlags.PositionProportional);

        GameField.Add(button);
        
        if (buttonX < min || buttonX > max || buttonY < min || buttonY > max)
        {
            throw new Exception();
        }
    }

	public void OnResetGameButtonClicked(object sender, EventArgs e)
	{
        GameTimer.Stop();
        GameField.RemoveAt(GameField.Count - 1);
        StartGame();
    }

    private async Task SaveResult(int result)
    {
        try
        {
            var gameResultData = new { Score = result, PlayerId = Preferences.Get("id", "") };
            string json = JsonSerializer.Serialize(gameResultData);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClientWrapper.PostAuthorizedAsync("/api/randompointsgameresult", body, Preferences.Get("token", ""));

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