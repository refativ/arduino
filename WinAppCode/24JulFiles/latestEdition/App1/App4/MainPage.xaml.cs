using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Windows;
using System.Net.Http;
using System.Threading.Tasks;

using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ItemsList = new List<string>();
            ItemsList.Add("1");
            ItemsList.Add("2");
            ItemsList.Add("3");
            this.NavigationCacheMode = NavigationCacheMode.Required;
            grid.Visibility = Visibility.Visible;
            grid.IsHitTestVisible = true;
            //  Error.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {


        }


   //     public string username { set; get; }
        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            prog.IsActive = true;
            Login.IsEnabled = false;
            usernametextbox.IsEnabled = false;
            passwordtextbox.IsEnabled = false;
            Testbutton.IsEnabled = false;

            int result = await GetData(usernametextbox.Text, passwordtextbox.Password.ToString(), 1);
            //0-> success 
            //1-> Not exists
            //2->Wrong password
            if (result == 0)
            {
                Globals.username = usernametextbox.Text.ToString();


                Error.Visibility = Visibility.Collapsed;
                //                Globals.InShelf1 = new List<string>() { "one", "uno" };
                //                 Globals.InShelf2 = new List<string>() { "two", "dos" };
                //                Globals.InShelf3 = new List<string>() { "three", "tred" };
                // fill up shelfs
             //   try
          //      {
                  //  int temp = await ShelfItems(usernametextbox.Text, 1);
         //       }
       //         catch (Exception ex)
        //        {


         //           string tempu = "roman";
      //              List<string> theList = new List<string>();
        //            theList.Add(tempu);


                         Globals.InShelf1 = await ShelfItems(Globals.username, 1);
                         Globals.InShelf2 = await ShelfItems(Globals.username, 2);
                         Globals.InShelf3 = await ShelfItems(Globals.username, 3);
                //  Globals.InShelf1 = theList;
                //  Globals.InShelf2 = theList;
                //  Globals.InShelf3 = theList;

                Login.IsEnabled = true;
                usernametextbox.IsEnabled = true;
                passwordtextbox.IsEnabled = true;
                Testbutton.IsEnabled = true;
                this.Frame.Navigate(typeof(BlankPage1));
                    prog.IsActive =false;


            }
                if (result == 1)
                {
                    Error.Text = "User doesn't exist";
                    Error.Visibility = Visibility.Visible;

                }
                if (result == 2)
                {
                    Error.Text = "Wrong password";
                    Error.Visibility = Visibility.Visible;
                }
            //Debug.Text = result.ToString();

            //   }
            prog.IsActive = false;
            Login.IsEnabled = true;
            usernametextbox.IsEnabled = true;
            passwordtextbox.IsEnabled = true;
            Testbutton.IsEnabled = true;
        }

        private void Testbutton_Click(object sender, RoutedEventArgs e)
        {
            prog.IsActive = true;
            Login.IsEnabled = false;
            usernametextbox.IsEnabled = false;
            passwordtextbox.IsEnabled = false;
            Testbutton.IsEnabled = false;
            this.Frame.Navigate(typeof(BlankPage3));



        }
     


        public async Task<int> GetData(string user, string pass, int function)
        {

            return await App.ShelfMobile.InvokeApiAsync<int>("shelf/Action", HttpMethod.Get, new Dictionary<string, string>() {
               { "user", user.ToString()} ,{ "pass", pass.ToString() },{ "function",function.ToString()} }
                );
        }


        List<string> ItemsList { get; set; }
    //    public int 

        public async Task<List<string>> ShelfItems(string user, int shelfno)
        {
            return await App.ShelfMobile.InvokeApiAsync<List<string>>("shelf/ShelfItems", HttpMethod.Get, new Dictionary<string, string>() {
               { "user", user.ToString()} ,{ "shelfno", shelfno.ToString() }}
                );
        }
        

        /*
        public async Task<int> GetAddOrRemoveItem(string item, string user, int shelf, int function)
        {
            return await App.ShelfMobile.InvokeApiAsync<int>("shelf/AddOrRemoveItem", HttpMethod.Get, new Dictionary<string, string>()  {
                { "item", item.ToString() }, { "user", user.ToString()}, { "shelf", shelf.ToString()}, { "function", function.ToString()} });
        } */
    }
}