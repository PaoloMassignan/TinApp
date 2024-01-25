using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using TinApp.ViewModels;

namespace TinApp.Views
{
    public partial class ExerciseDetailPage : ContentPage
    {
        public ExerciseDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}