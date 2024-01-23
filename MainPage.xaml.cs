namespace iOSNavigationPageLeak;

public partial class MainPage
{
    public static WeakReference? NavigationPageRef { get; set; }
    public static WeakReference? NavigationPageHandlerRef { get; set; }
    public static WeakReference? StartPageRef { get; set; }
    public static WeakReference? StartPageHandlerRef { get; set; }

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
        if (NavigationPageHandlerRef == null)
            throw new InvalidOperationException($"{nameof(NavigationPageHandlerRef)} is null");
        if (StartPageHandlerRef == null)
            throw new InvalidOperationException($"{nameof(StartPageHandlerRef)} is null");
        
        ForceGCButton.IsEnabled = false;
        for (var i = 0; i < 20; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!NavigationPageRef.IsAlive)
                NavLabel.Text = $"🗑️➡️🚚 {nameof(NavigationPage)} was collected after {i + 1} collections.";
            
            if (!NavigationPageHandlerRef.IsAlive)
                NavHandlerLabel.Text = $"🗑️➡️🚚 {nameof(NavigationPage)} handler was collected after {i + 1} collections.";
            
            if (!StartPageRef.IsAlive)
                PageLabel.Text = $"🗑️➡️🚚 {nameof(StartPage)} was collected after {i + 1} collections.";
            
            if (!StartPageHandlerRef.IsAlive)
                PageHandlerLabel.Text = $"🗑️➡️🚚 {nameof(StartPage)} handler was collected after {i + 1} collections.";

            if (!StartPageRef.IsAlive && !NavigationPageRef.IsAlive
                && !StartPageHandlerRef.IsAlive && !NavigationPageHandlerRef.IsAlive)
                break;
            
            NavLabel.Text = $"💦 {nameof(NavigationPage)} is still alive after {i + 1} collections.";
            PageLabel.Text = $"💦 {nameof(StartPage)} is still alive after {i + 1} collections.";
            NavHandlerLabel.Text = $"💦 {nameof(NavigationPage)} handler is still alive after {i + 1} collections.";
            PageHandlerLabel.Text = $"💦 {nameof(StartPage)} handler is still alive after {i + 1} collections.";

            await Task.Delay(1000);
        }
        
        ForceGCButton.IsEnabled = true;
    }
}