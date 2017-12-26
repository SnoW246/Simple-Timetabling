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
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;  // offline sync
using Microsoft.WindowsAzure.MobileServices.Sync;         // offline sync
#endif

namespace simpleTimetabling
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<TodoItem, TodoItem> items;
        #if OFFLINE_SYNC_ENABLED
        private IMobileServiceSyncTable<TodoItem> todoTable = App.MobileService.GetSyncTable<TodoItem>(); // offline sync
        #else
        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();
    #endif

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
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
#if OFFLINE_SYNC_ENABLED
            await InitLocalStoreAsync(); // offline sync
#endif
            ButtonRefresh_Click(this, null);
        }

        private async Task InsertTodoItem(TodoItem todoItem)
        {
            // This code inserts a new TodoItem into the database. After the operation completes
            // and the mobile app backend has assigned an id, the item is added to the CollectionView.
            await todoTable.InsertAsync(todoItem);
            items.Add(todoItem);

#if OFFLINE_SYNC_ENABLED
            await App.MobileService.SyncContext.PushAsync(); // offline sync
#endif
        }

        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems.
                items = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                //ListItems.ItemsSource = items;
                //this.ButtonSave.IsEnabled = true;
            }
        }

        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database.
			// After the MobileService client responds, the item is removed from the list.
            await todoTable.UpdateAsync(item);
            items.Remove(item);
            //ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

#if OFFLINE_SYNC_ENABLED
            await App.MobileService.SyncContext.PushAsync(); // offline sync
#endif
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //ButtonRefresh.IsEnabled = false;

#if OFFLINE_SYNC_ENABLED
            await SyncAsync(); // offline sync
#endif
            await RefreshTodoItems();

            //ButtonRefresh.IsEnabled = true;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //var todoItem = new TodoItem { Text = TextInput.Text };
            //TextInput.Text = "";
            //await InsertTodoItem(todoItem);
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;
            await UpdateCheckedTodoItem(item);
        }

        private void TextInput_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            //if (e.Key == Windows.System.VirtualKey.Enter) {
            //    ButtonSave.Focus(FocusState.Programmatic);
            //}
        }

        private void AddNewBtn_Click(object sender, RoutedEventArgs e)
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


            tb.Text = Abbreviation.Text.ToString().ToUpper() + "(" + Name.Text + ")" + "\r\n" +
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
            }// End of switch
        }     
    }
}
