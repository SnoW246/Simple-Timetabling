using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace simpleTimetabling
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Timetables, Timetables> items;
        private IMobileServiceTable<Timetables> azureTable = App.MobileService.GetTable<Timetables>();
        //private IMobileServiceTable<Monday> azureMondayTable = App.MobileService.GetTable<Monday>();
        //private IMobileServiceTable<Tuesday> azureTuesdayTable = App.MobileService.GetTable<Tuesday>();
        //private IMobileServiceTable<Wednesday> azureWednesdayTable = App.MobileService.GetTable<Wednesday>();
        //private IMobileServiceTable<Thursday> azureThursdayTable = App.MobileService.GetTable<Thursday>();
        //private IMobileServiceTable<Friday> azureFridayTable = App.MobileService.GetTable<Friday>();
        //private IMobileServiceTable<Saturday> azureSaturdayTable = App.MobileService.GetTable<Saturday>();
        //private IMobileServiceTable<Sunday> azureSundayTable = App.MobileService.GetTable<Sunday>();

        public MainPage()
        {
            InitializeComponent();
            LoadAsync();
        }

        public async Task LoadAsync()
        {
            //items = await azureTable.Take(5).ToCollectionAsync();
            //await new Windows.UI.Popups.MessageDialog(
            //    items.LastOrDefault().ID
            //    + items.LastOrDefault().Lecture
            //    + items.LastOrDefault().Lecturer
            //    + items.LastOrDefault().Name
            //    + items.LastOrDefault().Time).ShowAsync();

        }

        //public async Task UploadAsync(String name, String day, String lecture, String lecturer, String time)
        public async Task UploadAsync(String userID, String tuesday)
        {
            var newItem = new Timetables
            {
                UserID = userID,
                Tuesday = tuesday
            };
            await azureTable.InsertAsync(newItem);
            //var newItem = new Timetables {
            //    Name = name,
            //    Day = day,
            //    Lecture = lecture,
            //    Lecturer = lecturer,
            //    Time = time
            //};

            //await azureTable.InsertAsync(newItem);
        }

        private async void AddNewBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Day.SelectionBoxItem != null)
            {
                Border b = new Border();
                b.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.DarkGray);
                b.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White);
                b.CornerRadius = new CornerRadius(2);
                b.BorderThickness = new Thickness(2);
                Thickness bMargin = b.Margin;
                bMargin.Top = 2;
                b.Margin = bMargin;

                TextBlock tb = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    Width = 150,
                    IsHoldingEnabled = true
                };
                // Set margin of the textblock
                Thickness tbMargin = tb.Margin;
                Thickness padding = tb.Padding;
                tbMargin.Left = 2;
                padding.Top = 10;
                tb.Margin = tbMargin;
                tb.Padding = padding;

                
                    
                var element = Abbreviation.Text.ToString().ToUpper() + " (" + Name.Text + ")" + "\r\n" +
                    Type.SelectionBoxItem.ToString() + "\r\n" + Place.Text + "\r\n" +
                    StartTimeHour.SelectionBoxItem.ToString() + ":" + StartTimeMin.SelectionBoxItem.ToString() + " - " +
                    EndTimeHour.SelectionBoxItem.ToString() + ":" + EndTimeMin.SelectionBoxItem.ToString() + "\r\n" +
                    Lecturer.Text;

                tb.Text = element;

                // Add to Azure DB async!
                await UploadAsync("SnoW246", element);


                //tb.Text = "Name: " + Name.Text + "\r\n";
                //tb.Text += "Abbreviation: " + Abbreviation.Text.ToString().ToUpper() + "\r\n";
                //tb.Text += "Day: " + Day.SelectionBoxItem.ToString() + "\r\n";
                //tb.Text += "Place: " + Place.Text + "\r\n";
                //tb.Text += "Start Time: " + StartTimeHour.SelectionBoxItem.ToString() + ":" + StartTimeMin.SelectionBoxItem.ToString() + "\r\n";
                //tb.Text += "End Time: " + EndTimeHour.SelectionBoxItem.ToString() + ":" + EndTimeMin.SelectionBoxItem.ToString() + "\r\n";
                //tb.Text += "Lecturer: " + Lecturer.Text + "\r\n";
                //tb.Text += "Type: " + Type.SelectionBoxItem.ToString();

                string caseSwitch = Day.SelectionBoxItem.ToString();
                switch (caseSwitch)
                {
                    case "Monday":
                        b.Child = tb;
                        mondayStack.Children.Add(b);
                        break;
                    case "Tuesday":
                        b.Child = tb;
                        tuesdayStack.Children.Add(b);
                        break;
                    case "Wednesday":
                        b.Child = tb;
                        wednesdayStack.Children.Add(b);
                        break;
                    case "Thursday":
                        b.Child = tb;
                        thursdayStack.Children.Add(b);
                        break;
                    case "Friday":
                        b.Child = tb;
                        fridayStack.Children.Add(b);
                        break;
                    case "Saturday":
                        b.Child = tb;
                        saturdayStack.Children.Add(b);
                        break;
                    case "Sunday":
                        b.Child = tb;
                        sundayStack.Children.Add(b);
                        break;
                    default:
                        Windows.UI.Popups.MessageDialog msg = new Windows.UI.Popups.MessageDialog("Unexpected error!", "Critical Warning");
                        msg.ShowAsync();
                        break;
                }// End of switch
            }
            else
            {
                Windows.UI.Popups.MessageDialog msgWarning = new Windows.UI.Popups.MessageDialog("You must fill in the blanks!", "Error!");
                msgWarning.ShowAsync();
            }

            //UploadAsync(Name.Text, day, lecture, lecturer, time);
        }
    }
}
