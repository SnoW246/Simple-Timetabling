using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace simpleTimetabling
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistrationPage));
        }

        private void Sign_in_Click(object sender, RoutedEventArgs e)
        {
            // Take username and password and compare against azure database to validate the user

            Frame.Navigate(typeof(MainPage));
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