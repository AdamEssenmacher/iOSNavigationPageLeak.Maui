namespace iOSNavigationPageLeak;

public partial class StartPage
{
    public StartPage()
    {
        InitializeComponent();
    }

    private void ReplaceMainPage(object? sender, EventArgs e)
    {
        var navigationPage = (NavigationPage)Parent;
        MainPage.NavigationPageRef = new WeakReference(navigationPage);
        MainPage.StartPageRef = new WeakReference(this);
        MainPage.NavigationPageHandlerRef = new WeakReference(navigationPage.Handler);
        MainPage.StartPageHandlerRef = new WeakReference(Handler);
        
        // This has no effect on the leaks
        // Handler?.DisconnectHandler();
        
        // This prevents the leaks
        // navigationPage.Handler?.DisconnectHandler();

        Application.Current.MainPage = new MainPage();
    }
}