using Microsoft.WindowsAzure.MobileServices;
using simpleTimetabling.DataModel;
using System;
using System.Collections.Generic;
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
        //public interface IDomainManager<TData> where TData : class, ITableData

        public MainPage()
        {
            InitializeComponent();
            LoadAsync();
        }

        public async Task LoadAsync()
        {
            int count = 0;
            do
            {
                items = await azureTable.Take(1).ToCollectionAsync();
                    for(int i = 0; i < items.Count; i++){
                        items = await azureTable.Take((items.Count+1)).ToCollectionAsync();
                        count++;
                    }
            } while (items.Count != count);

            var itemsCP = items;

            List<string> elementsMonday = new List<string>();
            elementsMonday = items.Where(x => x.Monday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Monday).ToList();
            int sizeMonday = elementsMonday.Count();
            for (int i = 0; i < sizeMonday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsMonday.ElementAt(i));
                b = GenerateNewBorder(tb);

                mondayStack.Children.Add(b);
            }
            items = itemsCP;

            List<string> elementsTuesday = new List<string>();
            elementsTuesday = items.Where(x => x.Tuesday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Tuesday).ToList();
            int sizeTuesday = elementsTuesday.Count();
            for (int i = 0; i < sizeTuesday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsTuesday.ElementAt(i));
                b = GenerateNewBorder(tb);
                tuesdayStack.Children.Add(b);
            }
            items = itemsCP;

            List<string> elementsWednesday = new List<string>();
            elementsWednesday = items.Where(x => x.Wednesday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Wednesday).ToList();
            int sizeWednesday = elementsWednesday.Count();
            for (int i = 0; i < sizeWednesday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsWednesday.ElementAt(i));
                b = GenerateNewBorder(tb);
                wednesdayStack.Children.Add(b);
            }
            items = itemsCP;

            List<string> elementsThursday = new List<string>();
            elementsThursday = items.Where(x => x.Thursday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Thursday).ToList();
            int sizeThursday = elementsThursday.Count();
            for (int i = 0; i < sizeThursday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsThursday.ElementAt(i));
                b = GenerateNewBorder(tb);
                thursdayStack.Children.Add(b);
            }
            items = itemsCP;

            List<string> elementsFriday = new List<string>();
            elementsFriday = items.Where(x => x.Friday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Friday).ToList();
            int sizeFriday = elementsFriday.Count();
            for (int i = 0; i < sizeFriday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsFriday.ElementAt(i));
                b = GenerateNewBorder(tb);
                fridayStack.Children.Add(b);
            }
            items = itemsCP;

            List<string> elementsSaturday = new List<string>();
            elementsSaturday = items.Where(x => x.Saturday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Saturday).ToList();
            int sizeSaturday = elementsSaturday.Count();
            for (int i = 0; i < sizeSaturday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsSaturday.ElementAt(i));
                b = GenerateNewBorder(tb);
                saturdayStack.Children.Add(b);
            }
            items = itemsCP;

            List<string> elementsSunday = new List<string>();
            elementsSunday = items.Where(x => x.Sunday != null && x.UserID.Equals(UniqueUser.UniqueID)).Select(x => x.Sunday).ToList();
            int sizeSunday = elementsSunday.Count();
            for (int i = 0; i < sizeSunday; i++)
            {
                var tb = new TextBlock();
                var b = new Border();
                tb = GenerateNewTextBox(elementsSunday.ElementAt(i));
                b = GenerateNewBorder(tb);
                sundayStack.Children.Add(b);
            }
            items = itemsCP;
        }

        public async Task UploadAsync(String userID, String element, String day)
        {
            var newItem = new Timetables();
            switch (day)
            {
                case "Monday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Monday = element
                    };
                    break;
                case "Tuesday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Tuesday = element
                    };
                    break;
                case "Wednesday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Wednesday = element
                    };
                    break;
                case "Thursday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Thursday = element
                    };
                    break;
                case "Friday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Friday = element
                    };
                    break;
                case "Saturday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Saturday = element
                    };
                    break;
                case "Sunday":
                    newItem = new Timetables
                    {
                        UserID = userID,
                        Sunday = element
                    };
                    break;
            }
            await azureTable.InsertAsync(newItem);
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

                var element = Abbreviation.Text.ToString().ToUpper() + " (" + Name.Text + ")" + "\r\n" +
                    Type.SelectionBoxItem.ToString() + "\r\n" + Place.Text + "\r\n" +
                    StartTimeHour.SelectionBoxItem.ToString() + ":" + StartTimeMin.SelectionBoxItem.ToString() + " - " +
                    EndTimeHour.SelectionBoxItem.ToString() + ":" + EndTimeMin.SelectionBoxItem.ToString() + "\r\n" +
                    Lecturer.Text;

                var tb = GenerateNewTextBox(element);

                string caseSwitch = Day.SelectionBoxItem.ToString();
                switch (caseSwitch)
                {
                    case "Monday":
                        b.Child = tb;
                        mondayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                    case "Tuesday":
                        b.Child = tb;
                        tuesdayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                    case "Wednesday":
                        b.Child = tb;
                        wednesdayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                    case "Thursday":
                        b.Child = tb;
                        thursdayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                    case "Friday":
                        b.Child = tb;
                        fridayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                    case "Saturday":
                        b.Child = tb;
                        saturdayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                    case "Sunday":
                        b.Child = tb;
                        sundayStack.Children.Add(b);
                        // Add to Azure DB async!
                        await UploadAsync(UniqueUser.UniqueID, element, caseSwitch);
                        break;
                }// End of switch
            }
            else
            {
                Windows.UI.Popups.MessageDialog msgWarning = new Windows.UI.Popups.MessageDialog("You must fill in the blanks!", "Error!");
                await msgWarning.ShowAsync();
            }
        }

        //protected virtual async Task DeleteAsync(string ID);

        private async void DeleteThisBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            var element = DeleteThisAbbreviation.Text.ToString().ToUpper() + " (" + DeleteThisName.Text + ")" + "\r\n" +
                    DeleteThisType.SelectionBoxItem.ToString() + "\r\n" + DeleteThisPlace.Text + "\r\n" +
                    DeleteThisStartTimeHour.SelectionBoxItem.ToString() + ":" + DeleteThisStartTimeMin.SelectionBoxItem.ToString() + " - " +
                    DeleteThisEndTimeHour.SelectionBoxItem.ToString() + ":" + DeleteThisEndTimeMin.SelectionBoxItem.ToString() + "\r\n" +
                    DeleteThisLecturer.Text;

            string caseSwitch = DeleteThisDay.SelectionBoxItem.ToString();
            switch (caseSwitch)
            {
                case "Monday":
                    var elementsMonday = await azureTable.ToListAsync();
                    var removeMondayElement = elementsMonday.Where(x => x.Monday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Monday.Equals(element.ToString()));
                    foreach (var item in removeMondayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
                case "Tuesday":
                    var elementsTuesday = await azureTable.ToListAsync();
                    var removeTuesdayElement = elementsTuesday.Where(x => x.Tuesday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Tuesday.Equals(element.ToString()));
                    foreach (var item in removeTuesdayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
                case "Wednesday":
                    var elementsWednesday = await azureTable.ToListAsync();
                    var removeWednesdayElement = elementsWednesday.Where(x => x.Wednesday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Wednesday.Equals(element.ToString()));
                    foreach (var item in removeWednesdayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
                case "Thursday":
                    var elementsThursday = await azureTable.ToListAsync();
                    var removeThursdayElement = elementsThursday.Where(x => x.Thursday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Thursday.Equals(element.ToString()));
                    foreach (var item in removeThursdayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
                case "Friday":
                    var elementsFriday = await azureTable.ToListAsync();
                    var removeFridayElement = elementsFriday.Where(x => x.Friday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Friday.Equals(element.ToString()));
                    foreach (var item in removeFridayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
                case "Saturday":
                    var elementsSaturday = await azureTable.ToListAsync();
                    var removeSaturdayElement = elementsSaturday.Where(x => x.Saturday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Saturday.Equals(element.ToString()));
                    foreach (var item in removeSaturdayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
                case "Sunday":
                    var elementsSunday = await azureTable.ToListAsync();
                    var removeSundayElement = elementsSunday.Where(x => x.Sunday != null && x.UserID.Equals(UniqueUser.UniqueID) && x.Sunday.Equals(element.ToString()));
                    foreach (var item in removeSundayElement)
                    {
                        await azureTable.DeleteAsync(item);
                    }
                    break;
            }// End of switch
            Frame.Navigate(typeof(MainPage));
        }

        public TextBlock GenerateNewTextBox(String element)
        {
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
            tb.Text = element;

            return tb;
        }

        public Border GenerateNewBorder(TextBlock tb)
        {
            Border b = new Border();
            b.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.DarkGray);
            b.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.White);
            b.CornerRadius = new CornerRadius(2);
            b.BorderThickness = new Thickness(2);
            Thickness bMargin = b.Margin;
            bMargin.Top = 2;
            b.Margin = bMargin;
            IsHoldingEnabled = true;

            b.Child = tb;
            return b;
        }
    }
}