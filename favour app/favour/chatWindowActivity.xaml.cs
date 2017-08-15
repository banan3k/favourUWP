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
using System.Diagnostics;
using Windows.UI.ViewManagement;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace favour
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class chatWindowActivity : Page
    {
        public chatWindowActivity()
        {
            this.InitializeComponent();

            widthScreen = (int)Window.Current.Bounds.Width;
            heightScreen = (int)Window.Current.Bounds.Height;

            requestText.AddHandler(PointerReleasedEvent, new PointerEventHandler(toolStripClick), true);

            requestText.Margin = new Thickness(0, heightScreen * 0.8, 0,0);
            requestText.Height = heightScreen * 0.2;
            requestText.Width = widthScreen;
            
            requestSpace.Height = 0;
            requestSpace.Margin = new Thickness(0, 0, 0, 0);

            scrollView.Height = heightScreen * 1;
            scrollView.Margin = new Thickness(0, 0, 0, 0);
            

            requestText.Height = 0;
            requestText.IsEnabled = false;
            requestText.Visibility = Visibility.Collapsed;
        }

        void changeValue(object sender, PointerRoutedEventArgs e)
        {
            if((string)((ToggleButton)(sender)).Content=="YES")
                ((ToggleButton)(sender)).Content = "NO";
            else
                ((ToggleButton)(sender)).Content = "YES";
        }
        int totalRequests = 0;
        int widthScreen = 0, heightScreen;
        public int howManyRequests = 5;

        private void checkDetails(object sender, PointerRoutedEventArgs e)
        {
            string[] temp = new string[2];
            temp[0] = ((TextBlock)sender).Text;
            temp[1] = passedParameter;
            this.Frame.Navigate(typeof(details), temp);
            
        }

        void getRequestForYou(int howMany)
        {
            
            if (howMany == 0)
                howMany = howManyRequests;
            for (int i=0; i< howMany; i++)
            {
                
                TextBlock text = new TextBlock();
                if (howMany == 1)
                    text.Text = requestG;
                else
                    text.Text = "bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla";
                text.Width = 0.45 * widthScreen;
                text.Height = 0.1 * heightScreen;
                text.Padding = new Thickness(0, 0, 0, 0);
                text.TextWrapping = TextWrapping.Wrap;
                text.Margin = new Thickness(0, 0, 0, 0);
                text.AddHandler(PointerReleasedEvent, new PointerEventHandler(checkDetails), true);


                Button newButton = new Button();
                newButton.Content = "YES";
                newButton.Name = i+"yes";
             //   newButton.IsChecked = true;
                newButton.Width = 0.2 * widthScreen;
                newButton.Height = 0.1 * heightScreen;
             //   newButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(changeValue), true);
                Thickness margin = newButton.Margin;

                Button newButton2 = new Button();
                newButton2.Content = "NO";
                newButton2.Name =i+ "no";
                //   newButton.IsChecked = true;
                newButton2.Width = 0.2 * widthScreen;
                newButton2.Height = 0.1 * heightScreen;
                //   newButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(changeValue), true);




                Border theLine = new Border();
                theLine.Width = 1.1 * widthScreen;
                theLine.Height = 0.1 * heightScreen;
                theLine.BorderThickness = new Thickness(1);
               
                Windows.UI.Xaml.Media.SolidColorBrush colorForChat = new Windows.UI.Xaml.Media.SolidColorBrush();
                Windows.UI.Xaml.Media.SolidColorBrush colorForChat2 = new Windows.UI.Xaml.Media.SolidColorBrush();
                Windows.UI.Color actualColors = new Windows.UI.Color();
                actualColors.R = 0;
                actualColors.G = 0;
                actualColors.B = 0;
                actualColors.A = 255;
                colorForChat.Color = actualColors;
                theLine.BorderBrush = colorForChat;
               
                margin.Left = widthScreen * 0.75;
              //    margin.Top = -(scrollView.Height/2)+ ((i+ totalRequests )* (0.25 * heightScreen));
                margin.Top = ((i + totalRequests) * (0.15 * heightScreen));
                

                newButton.Margin = margin;
                margin.Left = 0.05*widthScreen;
                newButton2.Margin = margin;

                margin.Left = -0.125 * widthScreen;
                text.Margin = margin;

                margin.Left = -0.2 * widthScreen;
                //   theLine.Margin = new Thickness(0, 0.15 * heightScreen, 0,0);
                theLine.Margin = margin;
                //      buttons.Add(newButton);
                //  this.Controls.Add(newButton);
                requestSpace.Children.Add(newButton);
                requestSpace.Children.Add(newButton2);
                requestSpace.Children.Add(text);
                requestSpace.Children.Add(theLine);
                //    Debug.WriteLine("size " + ((Button)requestSpace.Children[0]).Width);
                if (passedParameter == "0")
                {
                    newButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(setAnswer), true);
                    newButton2.AddHandler(PointerReleasedEvent, new PointerEventHandler(setAnswer), true);
                }
                else
                {
                    newButton.IsEnabled = false;
                    newButton2.IsEnabled = false;
                }
                //   requestSpace.Children[0].lo
                //   this.cont
                actualColors.R = 255;
                actualColors.G = 255;
                actualColors.B = 255;
                actualColors.A = 255;
                colorForChat.Color = actualColors;
                newButton.Background = colorForChat;
                newButton2.Background = colorForChat;

                newButton.VerticalAlignment = VerticalAlignment.Top;
                newButton2.VerticalAlignment = VerticalAlignment.Top;
                text.VerticalAlignment = VerticalAlignment.Top;
                theLine.VerticalAlignment = VerticalAlignment.Top;

                requestText.Background = colorForChat;

                actualColors.R = 0;
                actualColors.G = 0;
                actualColors.B = 0;
                actualColors.A = 255;
                colorForChat2.Color = actualColors;
                theLine.BorderBrush = colorForChat2;

                requestSpace.Height += (0.2 *  heightScreen);

            }
            
            totalRequests+=howMany;
            scrollView.ScrollToVerticalOffset(requestSpace.Height);
        }

        

        private void setAnswer(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Media.SolidColorBrush colorForChat = new Windows.UI.Xaml.Media.SolidColorBrush();
            Windows.UI.Xaml.Media.SolidColorBrush colorForChat2 = new Windows.UI.Xaml.Media.SolidColorBrush();
            Windows.UI.Color actualColors = new Windows.UI.Color();
        //    Windows.UI.Color actualColors2 = new Windows.UI.Color();
            if (((Windows.UI.Xaml.Media.SolidColorBrush)((Button)sender).Background).Color.G != 254)
            {
                actualColors.R = 0;
                actualColors.G = 254;
                actualColors.B = 0;
                actualColors.A = 255;
            }
            else
            {
                actualColors.R = 255;
                actualColors.G = 255;
                actualColors.B = 255;
                actualColors.A = 255;
            }
            colorForChat.Color = actualColors;
            ((Button)sender).Background = colorForChat;

            actualColors.R = 255;
            actualColors.G = 255;
            actualColors.B = 255;
            actualColors.A = 255;
            colorForChat2.Color = actualColors;
            if (((Button)sender).Name.Substring(1, 2) == "no")
            {
                int idForYes = ((int.Parse(((Button)sender).Name[0].ToString())) * 4 );
               // Debug.WriteLine(((int.Parse(((Button)sender).Name[0].ToString())) * 4 ));
                ((Button)requestSpace.Children[idForYes]).Background = colorForChat2;
            }
            else
            {
                int idForYes = ((int.Parse(((Button)sender).Name[0].ToString())) * 4 + 1);
               
                ((Button)requestSpace.Children[idForYes]).Background = colorForChat2;
            }
               
        }

        void getYourRequest()
        {
           // requestSpace.Margin = new Thickness(0, 100, 0, 0);
            requestText.Height = 64;
            requestText.IsEnabled = true;
            requestText.Visibility = Visibility.Visible;
            getRequestForYou(0);

          

        }

        string requestG;
        void sendRequest()
        {
            requestG = requestText.Text;
            Debug.WriteLine(" request sending for "+ requestG);
            getRequestForYou(1);

            InputPane.GetForCurrentView().TryHide();

            requestText.Text = "";
        }
        private void toolStripClick(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("clicked");
            InputPane.GetForCurrentView().TryShow();
        }

        private void requestText_KeyUp(object sender, KeyRoutedEventArgs e)
        {
           
            if(e.Key.ToString()=="Enter")
            {
            //    requestText.IsEnabled = false;
                // requestText.IsEnabled = true;
                // temp.Focus(FocusState.Programmatic);
               
               // this.Focus(FocusState.);
                sendRequest();
            }
        }

        string passedParameter;

       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passedParameter = e.Parameter.ToString();
            Debug.WriteLine("czesc "+ passedParameter);
            // idForChat = int.Parse(passedParameter);
            if (passedParameter=="0")
            {
                getRequestForYou(0);
            }
            else
            {
                getYourRequest();
            }
           // textBlock.Text = passedParameter;
        }

    }

    

}

