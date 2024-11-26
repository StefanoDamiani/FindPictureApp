using PictureExplorer.ViewModels;

namespace PictureExplorer.Views;

public partial class PictureDetailsPage : ContentPage, IQueryAttributable
{
    public PictureDetailsPage()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("url") && query.ContainsKey("id"))
        {
            var url = query["url"] as string;
            var id = query["id"] as string;
            BindingContext = new PictureDetailsViewModel(url, id);
        }
    }
}