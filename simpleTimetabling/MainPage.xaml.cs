/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=717898
 */
//#define OFFLINE_SYNC_ENABLED

using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace simpleTimetabling
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Timetables, Timetables> items;
        private IMobileServiceTable<Timetables> azureTable = App.MobileService.GetTable<Timetables>();

        public string NewName { get; set; }
        public string NewAbbreviation { get; set; }
        public string NewDay { get; set; }
        public string NewPlace { get; set; }
        public int NewStartTime { get; set; }
        public int NewEndTime { get; set; }
        //public int time { get; set; }
        //public int time { get; set; }
        public string NewLecture { get; set; }
        public string NewType { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            loadAsync();
        }

        public async Task loadAsync()
        {
            items = await azureTable.Take(5).ToCollectionAsync();
            new Windows.UI.Popups.MessageDialog(
                items.LastOrDefault().ID 
                + items.LastOrDefault().Lecture 
                + items.LastOrDefault().Lecturer 
                + items.LastOrDefault().Name 
                + items.LastOrDefault().Time).ShowAsync();
        }

        public async Task uploadAsync(String name, String day, String lecture, String lecturer, String time)
        {
            var newItem = new Timetables {
                Name = name,
                Day = day,
                Lecture = lecture,
                Lecturer = lecturer,
                Time = time
            };

            azureTable.InsertAsync(newItem);
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //var todoItem = new TodoItem { Text = TextInput.Text };
            //TextInput.Text = "";
            //await InsertTodoItem(todoItem);
        }

        private void TextInput_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            //if (e.Key == Windows.System.VirtualKey.Enter) {
            //    ButtonSave.Focus(FocusState.Programmatic);
            //}
        }

        private void AddNewBtn_Click(object sender, RoutedEventArgs e)
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
                    HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                    VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                    TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap,
                    Width = 150
                };
                // Set margin of the textblock
                Thickness tbMargin = tb.Margin;
                Thickness padding = tb.Padding;
                tbMargin.Left = 2;
                padding.Top = 10;
                tb.Margin = tbMargin;
                tb.Padding = padding;

                tb.Text = Abbreviation.Text.ToString().ToUpper() + " (" + Name.Text + ")" + "\r\n" +
                    Type.SelectionBoxItem.ToString() + "\r\n" + Place.Text + "\r\n" +
                    StartTimeHour.SelectionBoxItem.ToString() + ":" + StartTimeMin.SelectionBoxItem.ToString() + " - " +
                    EndTimeHour.SelectionBoxItem.ToString() + ":" + EndTimeMin.SelectionBoxItem.ToString() + "\r\n" +
                    Lecturer.Text;

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
                this.Frame.Navigate(typeof(LoginPage));
            }


            //uploadAsync(Name.Text, day, lecture, lecturer, time);

        }
    }
}
