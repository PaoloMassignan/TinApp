using System.Diagnostics;
using TinApp.ViewModels;
using TinApp.Views;

namespace TinApp
{
    public partial class AppShell : Shell
    {
        public static AppShell? CurrentAppShell { get; private set; } = default!;

        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
            //Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage));
            //Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            //Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            CurrentAppShell = this;
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Current != null)
            {
                Debug.WriteLine($"AppShell: source={args.Current.Location}, target={args.Target.Location}");
            }
        }

        public void SetRootPageTitle(string name)
        {
            RootItem.Title = name;
            RootItem.FlyoutItemIsVisible = true;
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}