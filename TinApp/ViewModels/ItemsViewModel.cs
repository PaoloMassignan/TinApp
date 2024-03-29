﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using TinApp.Models;
using TinApp.Services;
using TinApp.Views;

namespace TinApp.ViewModels
{
    public partial class ItemsViewModel : ObservableObject
    {
        readonly IDataStore<Item> dataStore;
        ILogger<ItemsViewModel> logger;

        public ObservableCollection<Item> Items { get; }

        public ItemsViewModel(IDataStore<Item> dataStore, ILogger<ItemsViewModel> logger)
        {
            this.dataStore = dataStore;
            this.logger = logger;
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            IsBusy = false;
        }

        [ObservableProperty]
        private Item? selectedItem = default;

        [ObservableProperty]
        private string? title;

        [ObservableProperty]
        private bool isBusy;


        [RelayCommand]
        private async void ItemSelectionChanged(object sender)
        {
            Item? item = sender as Item;
            if (item == null)
            {
                logger.LogWarning("item is null.");
                return;
            }
            logger.LogDebug($"item is {item.Name}");
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ExerciseDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        [RelayCommand]
        private async Task LoadItems()
        {
            logger.LogDebug($"IsBusy={IsBusy}");

            try
            {
                Items.Clear();
                var items = await dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("{ex}", ex);
            }
            finally
            {
                IsBusy = false;
                logger.LogDebug("Set IsBusy to false");
            }
        }

        async public void OnAppearing()
        {
            SelectedItem = null;
            await LoadItems();
        }
    }
}