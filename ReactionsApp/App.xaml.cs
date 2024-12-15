namespace ReactionsApp
{
    public partial class App : Application
    {
        public static Window Window { get; set; }

        public App()
        {
            InitializeComponent();            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window = new Window();
            Window.Page = new AppShell();
            return Window;
        }
    }
}