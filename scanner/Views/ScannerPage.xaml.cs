using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using scanner.ViewModels;
using Xamarin.Essentials;

namespace scanner.Views
{
    public partial class ScannerPage : ContentPage
    {
        private readonly CartViewModel cartViewModel;

        public ScannerPage()
        {
            InitializeComponent();
            cartViewModel = App.CartViewModel;
            BindingContext = cartViewModel;
        }

        private async void OnScanButtonClicked(object sender, System.EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    cartViewModel.AddToCart(result.Text);
                    await Shell.Current.GoToAsync("//CartPage");
                });
            };

            await Navigation.PushAsync(scanPage);
        }
        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");
            if (confirm)
            {
                Preferences.Set("IsLoggedIn", false);
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
