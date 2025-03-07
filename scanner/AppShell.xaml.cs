using System;
using System.Collections.Generic;
using scanner.ViewModels;
using scanner.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace scanner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent(); }
        private async void OnLogoutClicked(object sender, System.EventArgs e)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");
            if (confirm)
            {
                Preferences.Set("IsLoggedIn", false);
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
