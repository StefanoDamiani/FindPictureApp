using PictureExplorer.Models;
using PictureExplorer.ViewModels;

namespace PictureExplorer.Views;

public partial class SearchPage : ContentPage
{
    private readonly InfiniteScrollViewModel _viewModel;
    
    public SearchPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new InfiniteScrollViewModel();
    }
    
    private async void OnSearchButtonPressed(object sender, EventArgs e)
    {
        if(_viewModel is InfiniteScrollViewModel viewModel)
            await viewModel.ExecuteSearchAsync();
    }

    private async void OnThresholdReached(object? sender, EventArgs e)
    {
        if (!_viewModel.IsLoading)
        {
            await _viewModel.LoadMoreImagesAsync();
        }
    }
    
    private async void OnPictureSelected(object sender, SelectionChangedEventArgs e)
    {
        var selectedPicture = e.CurrentSelection.FirstOrDefault() as Picture;

        if (selectedPicture != null)
        {
            await Shell.Current.GoToAsync($"{nameof(PictureDetailsPage)}?url={selectedPicture.Url}&id={selectedPicture.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}