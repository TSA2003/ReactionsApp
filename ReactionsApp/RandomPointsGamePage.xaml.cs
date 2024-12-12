namespace ReactionsApp;

public partial class RandomPointsGamePage : ContentPage
{
	private const int GAME_DURATION_IN_SECONDS = 60;
    private readonly Color FILL_COLOR = Color.FromRgb(0, 0, 255);
    private readonly Color CLICKED_FILL_COLOR = Color.FromRgb(0, 255, 0);
    private const double RADIUS = 20;

    public Point CurrentCircleCenter { get; set; }
    public bool IsCurrentCircleSelected { get; set; }
    public IDispatcherTimer GameTimer { get; set; }
    public int Score { get; set; }

    public RandomPointsGamePage()
	{
		InitializeComponent();
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
		var drawable = GameField.Drawable as RandomPointsDrawable;
		var touchPoint = e.GetPosition(GameField);

		if (touchPoint is null)
		{
			return;
		}

		if (drawable.TrySelectCircle(touchPoint.Value))
		{
			Score++;
            GameField.Invalidate();
        }
    }
}