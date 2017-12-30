using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace simpleTimetabling
{
    public sealed partial class LoginPage : Page
    {
        private MobileServiceCollection<Users, Users> userCollection;
        private IMobileServiceTable<Users> azureUsersTable = App.MobileService.GetTable<Users>();
        private string UniqueID { get; set; }

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }

        private async void Sign_in_ClickAsync(object sender, RoutedEventArgs e)
        {
            // Take username and password and compare against azure database to validate the user
            var uName = usernameTxt.Text.ToString();
            var uPass = passwordBox.Password;

            userCollection = await azureUsersTable.ToCollectionAsync();
            var check = userCollection.FirstOrDefault(u => u.Username.Equals(uName) && u.Password.Equals(uPass));

            if (check == null)
            {
                var confirmation = new MessageDialog("Wrong username or password! Please try again.");
                confirmation.Title = "Wrong Input!";
                confirmation.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                var choice = await confirmation.ShowAsync();
            }
            else{
                UniqueID = check.ID;
                var confirmation = new MessageDialog("Match!" + ", " + UniqueID + ", " + check.Username.ToString() + ", ");
                confirmation.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
                var choice = await confirmation.ShowAsync();

                Frame.Navigate(typeof(MainPage));
            }
        }

        private async void Exit_ClickAsync(object sender, RoutedEventArgs e)
        {
            var confirmation = new MessageDialog("Are you sure you wish to terminate the application?");
            confirmation.Title = "Terminate?";
            confirmation.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            confirmation.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var choice = await confirmation.ShowAsync();

            if ((int)choice.Id == 0)
            {
                Application.Current.Exit();
            }
        }
    }
}