using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TinApp.Services
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

        public ImageSource? GetImage(string path)
        {
            //if (_cache.ContainsKey(path))
            //    return _cache[path];

            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(path);
            var img = ImageSource.FromStream(() => stream);
            _cache[path] = img;
            return img;
        }


    }
}
