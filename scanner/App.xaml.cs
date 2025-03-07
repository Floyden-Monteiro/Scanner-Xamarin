using Xamarin.Forms;
using scanner.Views;
using scanner.ViewModels;
using Xamarin.Essentials;

namespace scanner
{
    public partial class App : Application
    {
        public static CartViewModel CartViewModel { get; private set; }

        public App()
        {
            InitializeComponent();

            CartViewModel = new CartViewModel();
            if (Preferences.Get("IsLoggedIn", false))
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
