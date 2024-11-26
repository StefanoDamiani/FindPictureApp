using Newtonsoft.Json;

namespace PictureExplorer.Models;

public class Picture
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("owner")]
    public string Owner { get; set; }

    [JsonProperty("secret")]
    public string Secret { get; set; }

    [JsonProperty("server")]
    public string Server { get; set; }

    [JsonProperty("farm")]
    public string Farm { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("ispublic")]
    public bool Ispublic { get; set; }

    [JsonProperty("isfriend")]
    public bool Isfriend { get; set; }

    [JsonProperty("isfamily")]
    public bool Isfamily { get; set; }
    
    public string Url { get => $"https://live.staticflickr.com/{Server}/{Id}_{Secret}_w.jpg"; }
}