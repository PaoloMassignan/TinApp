using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using TinApp.Models;
using TinApp.Services;

namespace TinApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class ItemDetailViewModel : ObservableObject
    {
        readonly IDataStore<Item> dataStore;
        ILogger<ItemDetailViewModel> logger;

        public ItemDetailViewModel(IDataStore<Item> dataStore, ILogger<ItemDetailViewModel> logger)
        {
            this.dataStore = dataStore;
            this.logger = logger;
        }

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private string? id;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private ImageSource? img;

        [ObservableProperty]
        private List<ImageSource?> imgs;

        [ObservableProperty]
        private List<string> imgSources;

        private string? itemId;
        public string ItemId
        {
            get => itemId;
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async Task LoadItemId(string itemId)
        {
            if (itemId == null) { throw new ArgumentNullException(nameof(itemId)); }
            var item = await dataStore.GetItemAsync(itemId);
            if (item == null) { logger.LogDebug("cannot find {itemId}", itemId); return; }
            Id = item.Id;
            Name = item.Name;
            Imgs = item.ImgSources.Select(x=>ImageService.Instance.GetImage(x)).ToList();
            ImgSources = item.ImgSources;
        }
    }
}
