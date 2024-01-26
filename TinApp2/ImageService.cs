using Microsoft.Maui;
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


        private string GetPrefix(string x)
        {
            int firstIndex = x.IndexOf("Exercises.") + "Exercises.".Length;
            int secondIndex = x.IndexOf(".", firstIndex);
            return x.Substring(firstIndex, secondIndex - firstIndex);
        }

        private string GetPath(string x)
        {
            int firstIndex = x.LastIndexOf(".");
            int secondIndex = x.LastIndexOf(".", firstIndex - 1) + 1;
            return x.Substring(secondIndex);
        }


        private ImageService()
        {
            Assembly a = Assembly.GetExecutingAssembly();
            var exercises = new List<string>();
            foreach (var resource in a.GetManifestResourceNames())
            {
                if (!resource.Contains("Exercises"))
                    continue;
                exercises.Add(resource);
            }
            exercises.GroupBy(x => GetPrefix(x)).ToList().ForEach(x => _imageSources.Add(x.Key, x.ToList()));
        }

        private Dictionary<string, List<string>> _imageSources = new Dictionary<string, List<string>>();

        public int Count => _imageSources.Count;

        public List<string> GetCategories()
        {
            return _imageSources.Keys.ToList();
        }
        public List<string> GetImageOfCategory(string key)
        {
            return _imageSources[key].Select(x => GetImage(x)).ToList() ;
        }
        private string GetImage(string path)
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
