using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinApp2
{
    public class ImageService
    {
        private static ImageService instance;
        public static ImageService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ImageService();
                return instance;
            }
        }

        private Dictionary<string, ImageSource?> _cache = new Dictionary<string, ImageSource?>();

        public string GetImage(string path)
        {
            //if (_cache.ContainsKey(path))
            //    return _cache[path];

            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(path);
            byte[] imageBytes = new byte[stream.Length];
            stream.Read(imageBytes, 0, imageBytes.Length);
            var imageSource = Convert.ToBase64String(imageBytes);
            imageSource = string.Format("data:image/png;base64,{0}", imageSource);
            return imageSource;
        }


    }
}
