using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using TinApp.Models;

namespace TinApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            Assembly a = Assembly.GetExecutingAssembly();
            var exercises = new List<string>();
            foreach (var resource in a.GetManifestResourceNames())
            {
                if (!resource.Contains("Exercises"))
                    continue;
               exercises.Add(resource);
            }
            var groupedExercises = exercises.GroupBy(x => GetPrefix(x));
            foreach(var group in groupedExercises)
            {
                items.Add(new Item() { Path = group.Key, ImgSources = group.Select(x=>GetPath(x)).ToList() });
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
            int secondIndex = x.LastIndexOf(".", firstIndex - 1)+1;
            return x.Substring(secondIndex);
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null) { throw new ArgumentNullException(nameof(item)); }
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null) { throw new ArgumentNullException(nameof(item)); }

            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            if (oldItem != null)
            {
                items.Remove(oldItem);
                items.Add(item);
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (id == null) { throw new ArgumentNullException(nameof(id)); }

            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            if (oldItem != null)
            {
                items.Remove(oldItem);
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<Item?> GetItemAsync(string id)
        {
            if (id == null) { throw new ArgumentNullException(nameof(id)); }

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}