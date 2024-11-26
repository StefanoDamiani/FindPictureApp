using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using PictureExplorer.Models;
using PictureExplorer.Services;

namespace PictureExplorer.ViewModels
{
    public class InfiniteScrollViewModel : BaseViewModel
    {
        private int _currentPage = 0;
        private bool _isBusy = false;
        private bool _isLoading = false;
        private string _searchQuery = string.Empty;
        private ObservableCollection<Picture> _pictures;
        
        public ObservableCollection<Picture> Pictures
        {
            get => _pictures;
            set => SetProperty(ref _pictures, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }
        
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public InfiniteScrollViewModel()
        {
            Pictures = new ObservableCollection<Picture>();
        }

        void ResetSearch()
        {
            Pictures.Clear();
            CurrentPage = 0;
        }
        
        public async Task ExecuteSearchAsync()
        {
            ResetSearch();
            await LoadMoreImagesAsync();
        }

        public async Task LoadMoreImagesAsync()
        {
            if (_isBusy || string.IsNullOrWhiteSpace(SearchQuery) || IsLoading)
                return;
            
            _isBusy = true;
            IsLoading = true;
            
            CurrentPage++;
            
            var response = await Flickr.SearchFlickrImages(SearchQuery, CurrentPage);
            var pics = ParseResponse(response);
            
            AddToCollection(pics);
            
            _isBusy = false;
            IsLoading = false;
        }

        private void AddToCollection(List<Picture> pics)
        {
            foreach (var pic in pics)
                Pictures.Add(pic);
        }

        List<Picture> ParseResponse(string response)
        {
            var jsonResponse = JObject.Parse(response);
            var photos = jsonResponse["photos"]?["photo"] ?? throw new InvalidOperationException("Can't convert photo data into JToken object");
            
            if (!photos.HasValues)
                return new List<Picture>();
            
            return photos.Select(BuildPicture).ToList();
        }

        Picture BuildPicture(JToken photo)
        {
            Picture picture = new Picture();
            picture.Farm = photo["farm"]?.ToString();
            picture.Server = photo["server"]?.ToString();
            picture.Id = photo["id"]?.ToString();
            picture.Secret = photo["secret"]?.ToString();
            return picture;
        }
    }
}