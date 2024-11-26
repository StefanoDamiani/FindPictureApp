using PictureExplorer.Services;

namespace PictureExplorer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Flickr.Init();
        
        Routing.RegisterRoute(nameof(Views.SearchPage), typeof(Views.SearchPage));
        Routing.RegisterRoute(nameof(Views.PictureDetailsPage), typeof(Views.PictureDetailsPage));
    }
}