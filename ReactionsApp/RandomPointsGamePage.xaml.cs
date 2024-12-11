namespace ReactionsApp;

public partial class RandomPointsGamePage : ContentPage
{
	private const int GAME_DURATION_IN_SECONDS = 60;

    public RandomPointsDrawable Drawable { get; set; }
    public IDispatcherTimer GameTimer { get; set; }
    public int Score { get; set; }

    public RandomPointsGamePage()
	{
		InitializeComponent();
		Drawable = new();
		BindingContext = this;
		StartGame();
	}

	private void StartGame()
	{
		GameTimer = Dispatcher.CreateTimer();
		GameTimer.Interval = TimeSpan.FromSeconds(GAME_DURATION_IN_SECONDS);
		GameTimer.Tick += (s, e) =>
		{

		};
		GameTimer.IsRepeating = false;
		GameTimer.Start();
	}

	public void OnGameFieldTap(object sender, TappedEventArgs e)
	{
		var touchPoint = e.GetPosition(GameField);
		
		GameField.Invalidate();
	}
}