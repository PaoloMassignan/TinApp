using TinApp.Services;
using TinApp.ViewModels;
using TinApp.Views;

namespace TinApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));  
            MainPage = new AppShell();
        }

    }
}