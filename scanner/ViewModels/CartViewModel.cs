using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using scanner.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace scanner.ViewModels
{
    public class CartViewModel
    {
        public ObservableCollection<CartItem> CartItems { get; private set; }

        public ICommand RemoveFromCartCommand { get; }

        public CartViewModel()
        {
            CartItems = LoadCartItems();
            RemoveFromCartCommand = new Command<CartItem>(RemoveFromCart);
        }

        public void AddToCart(string productName)
        {
            var item = CartItems.FirstOrDefault(i => i.ProductName == productName);
            if (item != null)
                item.Quantity++;
            else
                CartItems.Add(new CartItem { ProductName = productName, Quantity = 1 });

            SaveCartItems();
        }

        private async void RemoveFromCart(CartItem item)
        {
            if (item == null) return;

            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to remove {item.ProductName}?",
                "Yes", "No");

            if (isConfirmed)
            {
                CartItems.Remove(item);
                SaveCartItems();
            }
        }
        private void SaveCartItems()
        {
            Preferences.Set("CartItems", Newtonsoft.Json.JsonConvert.SerializeObject(CartItems));
        }

        private ObservableCollection<CartItem> LoadCartItems()
        {
            var cartItemsJson = Preferences.Get("CartItems", string.Empty);
            return string.IsNullOrEmpty(cartItemsJson)
                ? new ObservableCollection<CartItem>()
                : Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<CartItem>>(cartItemsJson);
        }
    }
}
