using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace simpleTimetabling
{
    public sealed partial class RegistrationPage : Page
    {
        //private MobileServiceCollection<Users, Users> items;
        private IMobileServiceTable<Users> azureUsersTable = App.MobileService.GetTable<Users>();

        public RegistrationPage()
        {
            InitializeComponent();
        }

        public async Task UploadAsync(String name, String surname, String dob, String username, String password)
        {
            var newUser = new Users
            {
                Name = name,
                Surname = surname,
                DOB = dob,
                Username = username,
                Password = password
            };
            await azureUsersTable.InsertAsync(newUser);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Validation of content required
            // If username already used, check async and tell user with messagege box
            //azureUsersTable.Where(usernameTxt.Text.Equals(azureUsersTable.Username));
            //usernameTxt.Text.

            if (nameTxt.Text.Equals(""))
            {
                infoTxt.Text = "One or more fields are empty. You must fill out all of the fields below!";
                nameTxt.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (surnameTxt.Text.Equals(""))
            {
                infoTxt.Text = "One or more fields are empty. You must fill out all of the fields below!";
                surnameTxt.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (dobDay.SelectionBoxItem == null)
            {
                infoTxt.Text = "One or more fields are empty. You must fill out all of the fields below!";
                dobDay.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (dobMonth.SelectionBoxItem == null)
            {
                infoTxt.Text = "One or more fields are empty. You must fill out all of the fields below!";
                dobMonth.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (dobYear.SelectionBoxItem == null)
            {
                infoTxt.Text = "One or more fields are empty. You must fill out all of the fields below!";
                dobYear.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (usernameTxt.Text.Equals(""))
            {
                infoTxt.Text = "One or more fields are empty. You must fill out all of the fields below!";
                usernameTxt.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }

            //Password match validation
            if (passwordBox.Password != passwordConfirmBox.Password || passwordBox.Password.Equals(""))
            {
                infoTxt.Text = "Passwords do not match! Please try again.";
                passwordBox.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                passwordConfirmBox.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                // Declare DOB variable & collect relative selected box items
                var dob = dobDay.SelectionBoxItem + "/" + dobMonth.SelectionBoxItem + "/" + dobYear.SelectionBoxItem;
                // Upload data to Azure Cloud Database table
                UploadAsync(nameTxt.Text, surnameTxt.Text, dob, usernameTxt.Text, passwordBox.Password);

                // Make a message box to thank for registration
                
                Frame.Navigate(typeof(LoginPage));
            }

        }

        private async void Return_ClickAsync(object sender, RoutedEventArgs e)
        {
            // Confirmation Box to ask if they wish to return
            var confirmation = new MessageDialog("Are you sure you wish to return to login page?");
            confirmation.Title = "Please confirm";
            confirmation.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            confirmation.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var choice = await confirmation.ShowAsync();

            if ((int)choice.Id == 0)
            {
                Frame.Navigate(typeof(LoginPage));
            }
            
        }
    }
}
