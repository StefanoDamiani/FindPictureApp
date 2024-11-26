using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PictureExplorer.Models;
using PictureExplorer.Services;

namespace PictureExplorer.ViewModels
{
    public class PictureDetailsViewModel : BaseViewModel
    {
        public string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public PictureDetails _details;
        public PictureDetails Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }
        
        public PictureDetailsViewModel(string pictureUrl, string id)
        {
            Url = pictureUrl;
            GetPictureDetails(id);
        }

        private async Task GetPictureDetails(string id)
        {
            var response = await Flickr.GetPictureInfo(id);
            Details = ParseResponse(response);
        }
        
        PictureDetails ParseResponse(string response)
        {
            var jsonResponse = JObject.Parse(response);
            var photo = jsonResponse.Value<JObject>("photo");
            
            JObject datesContent = photo.Value<JObject>("dates");
            string takenDate = datesContent.Value<string>("taken");
            
            JObject ownerContent = photo.Value<JObject>("owner");
            string owner = ownerContent.Value<string>("username");
            string location = ownerContent.Value<string>("location");
            
            JObject titleContent = photo.Value<JObject>("title");
            string title = titleContent.Value<string>("_content");
            
            
            return new PictureDetails()
            {
                Taken = "Taken Date : " + takenDate,
                Owner = "Owner : " + owner,
                Location = string.IsNullOrEmpty(location)?"Location : Unknown":"Location : " + location,
                Title = "Title : " + title
            };
        }
    }
}
