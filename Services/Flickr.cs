using RestSharp;

namespace PictureExplorer.Services;

public static class Flickr
{
    static Settings FlickrSettings { get; set; }

    private const string search_method = "flickr.photos.search";
    private const string getInfo_method = "flickr.photos.getInfo";

    struct Settings
    {
        public string ApiKey = string.Empty;
        public string Secret = string.Empty;
        public string Format = string.Empty;
        public bool NoJsonCallback = true;
        public int ElementPerPage = 15;

        public Settings() { }
    }

    public static void Init()
    {
        SetupSettings();
    }
    
    static void SetupSettings()
    {
        FlickrSettings = new Settings
        {
            ApiKey = "ed9292257a90be8366f527e7425d1735",
            Secret = "7e87f0f9c1805dcc",
            Format = "json",
            NoJsonCallback = true,
            ElementPerPage = 15
        };
    }
    
    public static async Task<string> SearchFlickrImages(string query, int page)
    {
        var client = new RestClient("https://api.flickr.com/services/rest/");
        var request = BuildBaseRequest(search_method);
        request.AddParameter("text", query);
        request.AddParameter("page", page);
        var response = await client.ExecuteAsync(request);
            
        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            throw new Exception("Issues with Flickr API: returned no results");
            
        return response.Content;
    }
    
    
    public static async Task<string> GetPictureInfo(string pictureId)
    {
        var client = new RestClient("https://api.flickr.com/services/rest/");
        var request = BuildBaseRequest(getInfo_method);
        request.AddParameter("photo_id", pictureId);
        var response = await client.ExecuteAsync(request);
            
        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            throw new Exception("Issues with Flickr API: returned no results");
            
        return response.Content;
    }
    
    static RestRequest BuildBaseRequest(string method)
    {
        var req = new RestRequest();
        req.AddParameter("method", method);
        req.AddParameter("api_key", FlickrSettings.ApiKey);
        req.AddParameter("format", FlickrSettings.Format);
        req.AddParameter("nojsoncallback", FlickrSettings.NoJsonCallback);
        req.AddParameter("per_page", FlickrSettings.ElementPerPage);
        req.AddParameter("sort", "relevance");
        return req;
    }
}