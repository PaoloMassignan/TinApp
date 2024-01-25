using System;
using System.Reflection;
using TinApp.Services;

namespace TinApp.Models
{
    public class Item
    {
        public string Id => Name;
        public string Name
        {
            get
            {
                return Path;
            }
        }

        private List<ImageSource?> _imgs = null;
        public List<ImageSource?> Imgs
        {
            get
            {
                if (_imgs == null)
                    _imgs = ImgSources.Select(x => ImageService.Instance.GetImage(x)).ToList();

                return _imgs;
            }
        }
        public ImageSource? FirstImg => Imgs.FirstOrDefault();

        public string Path { get; internal set; }
        public List<string> ImgSources { get; internal set; }
    }
}