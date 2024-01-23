namespace iOSNavigationPageLeak;

public partial class MainPage
{
    public static WeakReference? NavigationPageRef { get; set; }
    public static WeakReference? StartPageRef { get; set; }

    public MainPage()
    {
        InitializeComponent();
    }

    private async void ForceGC(object? sender, EventArgs e)
    {
        if (NavigationPageRef == null)
            throw new InvalidOperationException($"{nameof(NavigationPageRef)} is null");
        if (StartPageRef == null)
            throw new InvalidOperationException($"{nameof(StartPageRef)} is null");
        
        ForceGCButton.IsEnabled = false;
        for (var i = 0; i < 20; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!NavigationPageRef.IsAlive)
                NavLabel.Text = $"🗑️➡️🚚 {nameof(NavigationPage)} was collected after {i + 1} collections.";
            
            if (!StartPageRef.IsAlive)
                PageLabel.Text = $"🗑️➡️🚚 {nameof(StartPage)} was collected after {i + 1} collections.";

            if (!StartPageRef.IsAlive && !NavigationPageRef.IsAlive)
                break;
            
            NavLabel.Text = $"💦 {nameof(NavigationPage)} is still alive after {i + 1} collections.";
            PageLabel.Text = $"💦 {nameof(StartPage)} is still alive after {i + 1} collections.";

            await Task.Delay(1000);
        }
        
        ForceGCButton.IsEnabled = true;
    }
}