using System;
using System.Threading;
using System.Threading.Tasks;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public List<string> shlefs { set; get; }

        public BlankPage1()
        {
            this.DataContext = this;
            this.InitializeComponent();
            shlefs = new List<string>() { "1", "2", "3" };
            foreach (var a in shlefs) Shelfcombo.Items.Add(a);
            string TextToPresent = string.Format("Welcome {0}!", Globals.username);
            
            //  Shelfcombo.DataContext = Globals.currentList;
            if (Globals.SelectedShelf < 1)
                Shelfcombo.SelectedIndex = 0;
            else
            Shelfcombo.SelectedIndex = Globals.SelectedShelf-1;
            //ItemsCombo.DataContext = Globals.currentList;
           // ItemsCombo.SelectedIndex = 0;
         
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
          //  Shelfcombo.SelectedIndex = Globals.SelectedShelf-1;
        }

        private async void Shelf_Down_Button_Click(object sender, RoutedEventArgs e)
        {
            prog.IsActive = true;
            TakeShelfDown.IsEnabled = false;
            logout.IsEnabled = false;
            Shelfcombo.IsEnabled = false;
            ItemsCombo.IsEnabled = false;


            Globals.SelectedShelf =   Int32.Parse( Shelfcombo.SelectedItem.ToString());
            await Globals.SetSelected(Globals.SelectedShelf);
            switch (Globals.SelectedShelf)
            {
                case 1:
                    Globals.currentList = Globals.InShelf1;
                    await Task.Delay(5000);

                    //System.Threading.Tasks.
                    break;
                case 2:
                    Globals.currentList = Globals.InShelf2;
                    await Task.Delay(5000);
                    break;
                case 3:
                    Globals.currentList = Globals.InShelf3;
                    await Task.Delay(5000);
                    break;
                default:
                    break;
            }
            prog.IsActive = false;
            TakeShelfDown.IsEnabled = true;
            logout.IsEnabled =true;
            Shelfcombo.IsEnabled =true;
            ItemsCombo.IsEnabled =true;
          
            this.Frame.Navigate(typeof(BlankPage2));
        }



        private void Refresh_button_Click(object sender, RoutedEventArgs e)
        {

          
            Globals.SelectedShelf = Shelfcombo.SelectedIndex + 1;

            
                        switch (Globals.SelectedShelf)
                        {
                            case 1:
                                  ItemsCombo.Items.Clear();
  //                              if (Globals.InShelf1.Count != 0)
   //                             {
                                   foreach (var a in Globals.InShelf1)
                                    {
                          
                                            ItemsCombo.Items.Add(a);
                                    }
                                    Globals.currentList = Globals.InShelf1;
   //                             }
                                break;
                            case 2:
                                ItemsCombo.Items.Clear();
 //                               if (Globals.InShelf2.Count != 0)
   //                             {
                                    foreach (var a in Globals.InShelf2)
                                    {
                            
                                            ItemsCombo.Items.Add(a);
     //                               }
                                    Globals.currentList = Globals.InShelf2;
                                }
                                break;
                            case 3:
                                ItemsCombo.Items.Clear();
   //                             if (Globals.InShelf3.Count != 0)
    //                            {
                                    foreach (var a in Globals.InShelf3)
                                    {
                                
                                            ItemsCombo.Items.Add(a);
                                    }
                                    Globals.currentList = Globals.InShelf3;
     //                           }
                                break;
                            default:
                                ItemsCombo.Items.Clear();
                                break;
                                
         
        }
        
        //    ItemsCombo.Items.Clear();
            ItemsCombo.SelectedIndex = 0;
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Shelfcombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh_button_Click(sender, e);



        }
    }
}
