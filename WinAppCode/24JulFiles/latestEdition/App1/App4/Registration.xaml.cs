using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage3 : Page
    {
        public BlankPage3()
        {
            this.InitializeComponent();
        }


    

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            prog.IsActive = true;
            UserBox.IsEnabled = false;
            PassBox.IsEnabled = false;
            PassBox_Copy.IsEnabled = false;
            Register_link.IsEnabled = false;
            return_link.IsEnabled = false;
   
        

            //    PassWordBlock_Copy2.Text = result.ToString();

            if (PassBox.Password.ToString() != PassBox_Copy.Password.ToString())
            {
                Output.Text = "Passwords are not equal";
                prog.IsActive = false;
                UserBox.IsEnabled = true;
                PassBox.IsEnabled = true;
                PassBox_Copy.IsEnabled = true;
                Register_link.IsEnabled = true;
                return_link.IsEnabled = true;
                return;
            }
            int result = await ServiceFunc.GetDataSevice(UserBox.Text, PassBox.Password.ToString(), 2);
            if (result == 0)
            {
               
                Globals.username = UserBox.Text.ToString();
                Globals.InShelf1 = await ShelfItems(Globals.username, 1);
                Globals.InShelf2 = await ShelfItems(Globals.username, 2);
                Globals.InShelf3 = await ShelfItems(Globals.username, 3);


                this.Frame.Navigate(typeof(BlankPage1));
            }
            else if (result == 1)
            {
                Output.Text = "User name already exists";

            }

            prog.IsActive = false;
            UserBox.IsEnabled = true;
            PassBox.IsEnabled = true;
            PassBox_Copy.IsEnabled = true;
            Register_link.IsEnabled = true;
            return_link.IsEnabled = true;


        }
        public async Task<List<string>> ShelfItems(string user, int shelfno)
        {
            return await App.ShelfMobile.InvokeApiAsync<List<string>>("shelf/ShelfItems", HttpMethod.Get, new Dictionary<string, string>() {
               { "user", user.ToString()} ,{ "shelfno", shelfno.ToString() }}
                );
        }

        private void HyperlinkButton2_Click(object sender, RoutedEventArgs e)
        {
            prog.IsActive = false;
            UserBox.IsEnabled = true;
            PassBox.IsEnabled = true;
            PassBox_Copy.IsEnabled = true;
            Register_link.IsEnabled = true;
            return_link.IsEnabled = true;
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
