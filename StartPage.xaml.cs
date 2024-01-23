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

        Application.Current.MainPage = new MainPage();
    }
}