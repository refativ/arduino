using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using System.Net.Http;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class Globals
    {

        public static string username = "";
        public static bool IsShelfDown = false;
        public static int SelectedShelf = 0;
        public static List<string> InShelf1;
        public static List<string> InShelf2;
        public static List<string> InShelf3;
        public static int shelf1size = 0;
        public static int shelf2size = 0;
        public static int shelf3size = 0;
        public static List<string> currentList=null;
        public static async Task SetSelected(int shelfnum)
        {
            await App.ShelfMobile.InvokeApiAsync("shelf/ShelfNum", HttpMethod.Get, new Dictionary<string, string>() {
               { "selected", shelfnum.ToString()}}
              );
        }
    }

    public sealed partial class BlankPage2 : Page
    {
        public static int ir = 0;
        public BlankPage2()
        {
            
            this.InitializeComponent();
            ir++;
            this.NavigationCacheMode = NavigationCacheMode.Required;
            if (Globals.currentList != null)
            {
                foreach (var item in Globals.currentList)
                {
                    if (item.Trim() != "Dummy1" && item.Trim() != "Dummy2" && item.Trim() != "Dummy3")
                    {
                        comboBox.Items.Add(item);
                    }
                }

            }
           comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            comboBox.Items.Clear();
            foreach (var item in Globals.currentList) comboBox.Items.Add(item);
            comboBox.SelectedIndex = 0;
        }

        private async void Return_Click(object sender, RoutedEventArgs e)
        {
            prog.IsActive = true;
            comboBox.IsEnabled = false;
            addButton.IsEnabled = false;
            AddItemBox.IsEnabled = false;
            Remove_button.IsEnabled = false;
            Return.IsEnabled = false;


            switch (Globals.SelectedShelf)
            {
                case 1:
                  await  Task.Delay(5000);
                  break;
                case 2:
                    await Task.Delay(5000);
                    break;
                case 3:
                    await Task.Delay(5000);
                    break;

            }
              await Globals.SetSelected(-1);


            prog.IsActive = false;
            comboBox.IsEnabled = true;
            addButton.IsEnabled = true;
            AddItemBox.IsEnabled = true;
            Remove_button.IsEnabled = true;
            Return.IsEnabled = true;
            this.Frame.Navigate(typeof(BlankPage1));
        }
        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
        //    Return.Content = "sanity check"; 
            string item = AddItemBox.Text; // -- name of the item to add
            //string function = FunctionBox.Text;
            string username = Globals.username;
            int shelfno = Globals.SelectedShelf;
          
           comboBox.Items.Add(item);
           Globals.currentList.Add(item);
         if (shelfno == 1)
            {
                Globals.shelf1size++;
            }
        if (shelfno == 2)
            {
                Globals.shelf2size++;
            }
         if (shelfno == 3)
            {
                Globals.shelf3size++;
            }
            Globals.currentList.Remove("The Shelf Is Empty");
            comboBox.Items.Remove("The Shelf Is Empty");
            int tempRes = await AddItem(username, "The Shelf Is Empty", shelfno, "remove");
            int result = await AddItem(username, item, shelfno,"add");
            AddItemBox.Text = string.Empty;
            comboBox.SelectedIndex = 0;



        }
        
     


        public async Task<int> AddItem(string username, string item, int numshelf, string function)
        {
            return await App.ShelfMobile.InvokeApiAsync<int>("shelf/ShelfAddItem", HttpMethod.Get, new Dictionary<string, string>() {
               { "username", username.ToString()} ,{ "item", item.ToString() }, {"numshelf", numshelf.ToString() }, { "function", function.ToString() }
        }
                );
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private async void Remove_button_Click(object sender, RoutedEventArgs e)
        {
            string item = comboBox.SelectedItem.ToString();
            if (item == "The Shelf Is Empty")
            {
                return;
            }
            string username = Globals.username;
            int shelfno = Globals.SelectedShelf;


            Globals.currentList.Remove(item);
            comboBox.Items.Remove(item);
            int result = await AddItem(username, item, shelfno,"remove");
            if (shelfno == 1)
            {
                Globals.shelf1size--;
                if (Globals.shelf1size == 0)
                {
                    int tempRes = await AddItem(username, "The Shelf Is Empty", shelfno, "add");
                    Globals.currentList.Add("The Shelf Is Empty");
                    comboBox.Items.Add("The Shelf Is Empty");
                //    Globals.shelf1size++;
                 //   comboBox.SelectedIndex = 0;

                }
            }
            if (shelfno ==2)
            {
                Globals.shelf2size--;
                if (Globals.shelf2size == 0)
                {
                    int tempRes = await AddItem(username, "The Shelf Is Empty", shelfno, "add");
                    Globals.currentList.Add("The Shelf Is Empty");
                    comboBox.Items.Add("The Shelf Is Empty");
          //          Globals.shelf2size++;
                }
            }
            if (shelfno == 3)
            {
                Globals.shelf3size--;
                if (Globals.shelf3size == 0)
                {
                    int tempRes = await AddItem(username, "The Shelf Is Empty", shelfno, "add");
                    Globals.currentList.Add("The Shelf Is Empty");
                    comboBox.Items.Add("The Shelf Is Empty");
            ///        Globals.shelf3size++;

                }
            }
            comboBox.SelectedIndex = 0;


        }




        /*   working backup
                public async Task<int> AddItem(string username, string item, int numshelf)
                {
                    return await App.ShelfMobile.InvokeApiAsync<int>("shelf/ShelfAddItem", HttpMethod.Get, new Dictionary<string, string>() {
                       { "username", username.ToString()} ,{ "item", item.ToString() }, {"numshelf", numshelf.ToString() } }
                        );
                }
        */

        /*
                public async Task<int> RemoveItem(string username, string item, int numshelf)
                {
                    return await App.ShelfMobile.InvokeApiAsync<int>("shelf/ShelfRemoveItem", HttpMethod.Get, new Dictionary<string, string>() {
                       { "username", username.ToString()} ,{ "item", item.ToString() }, {"numshelf", numshelf.ToString() } }
                        );
                }
        */
    }
}
