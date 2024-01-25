using System.Diagnostics;
using TinApp.Models;
using TinApp.ViewModels;
using TinApp.Views;

namespace TinApp.Views
{
    public partial class ExercisePage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ExercisePage(ItemsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}