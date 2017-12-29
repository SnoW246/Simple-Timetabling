using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace simpleTimetabling
{
    public sealed partial class RegistrationPage : Page
    {
        private MobileServiceCollection<Users, Users> userCollection;
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

        private async void Register_ClickAsync(object sender, RoutedEventArgs e)
        {
            //await UsernameValidation();
            // Declaration of message variables
            var errorMessage1 = "One or more fields are empty. You must fill out all of the fields below!";
            var errorMessage2 = "Passwords do not match! Please try again.";
            var errorMessage3 = "One or more fields are empty. You cannot leave password fields empty!";
            var errorMessage4 = "Thank you for your registration. Now you can use your credentials to sign -in. Enjoy!";
            var errorTitle1 = "Invalid Input!";
            var errorTitle2 = "No Match!";
            var errorTitle4 = "Thank You!";
            int errorNo = 0;

            // Validation of content 
            if (nameTxt.Text.Equals("") || surnameTxt.Text.Equals("") || dobDay.SelectionBoxItem == null ||
                dobMonth.SelectionBoxItem == null || dobYear.SelectionBoxItem == null || usernameTxt.Text.Equals(""))
            {
                errorNo = 1;
                if (nameTxt.Text.Equals(""))
                {
                    nameTxt.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (surnameTxt.Text.Equals(""))
                {
                    surnameTxt.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (dobDay.SelectionBoxItem == null)
                {
                    dobDay.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (dobMonth.SelectionBoxItem == null)
                {
                    dobMonth.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (dobYear.SelectionBoxItem == null)
                {
                    dobYear.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (usernameTxt.Text.Equals(""))
                {
                    usernameTxt.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
            }// End of if

            //Password match validation
            if (passwordBox.Password != passwordConfirmBox.Password || passwordBox.Password.Equals("") || passwordConfirmBox.Password.Equals(""))
            {
                if (passwordBox.Password != passwordConfirmBox.Password)
                {
                    errorNo = 2;
                    passwordConfirmBox.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if(passwordBox.Password.Equals(""))
                {
                    errorNo += 1;
                    passwordBox.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
                if (passwordConfirmBox.Password.Equals(""))
                {
                    errorNo += 1;
                    passwordConfirmBox.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Red);
                }
            }// End of if

            // If username already used, check async and tell user with messagege box
            //azureUsersTable.Where(usernameTxt.Text.Equals(azureUsersTable.Username));
            //usernameTxt.Text.


            switch (errorNo)
            {
                case 1:
                    InformationMessage(errorMessage1, errorTitle1);
                    break;
                case 2:
                    InformationMessage(errorMessage2, errorTitle2);
                    break;
                case 3:
                    InformationMessage(errorMessage3, errorTitle1);
                    break;
                default:
                    break;
            }

            if (errorNo == 0)
            {
                // Declare DOB variable & collect relative selected box items
                var dob = dobDay.SelectionBoxItem + "/" + dobMonth.SelectionBoxItem + "/" + dobYear.SelectionBoxItem;
                // Upload data to Azure Cloud Database table
                await UploadAsync(nameTxt.Text, surnameTxt.Text, dob, usernameTxt.Text, passwordBox.Password);

                // Message box to thank for registration
                InformationMessage(errorMessage4, errorTitle4);
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

        private async void InformationMessage(String message, String title)
        {
            var confirmation = new MessageDialog(message);
            confirmation.Title = title;
            confirmation.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            var choice = await confirmation.ShowAsync();
        }

    }
}
