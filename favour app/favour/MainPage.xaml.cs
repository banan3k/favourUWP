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
using Windows.UI.Xaml;
using Windows.UI.Notifications;
using NotificationsExtensions.Toasts;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace favour
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = new  ViewModel.viewModelFirst();
            
           // var callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

            createChatsView(5);

            DispatcherTimerSetup();
            //notifications
            notify();
        }



        private void Show(ToastContent content)
        {
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(content.GetXml()));

            var template = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text01);
            var childNode = template?.GetElementsByTagName("text");
            childNode[0].InnerText = "You have new message";
            TileNotification tileNotification = new TileNotification(template);
            TileUpdater updateMgr = TileUpdateManager.CreateTileUpdaterForApplication();
            updateMgr.Update(tileNotification);
        }
        private void notify()
        {
            Show(new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    TitleText = new ToastText() { Text = "Take Out Garbage" },
                    BodyTextLine1 = new ToastText() { Text = "7:00 PM" }
                },

                Launch = "984910",

                Scenario = ToastScenario.Reminder,

                Actions = new ToastActionsSnoozeAndDismiss()
            });
        }
       

        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 10;

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //IsEnabled defaults to false
            Debug.WriteLine("dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            Debug.WriteLine("Calling dispatcherTimer.Start()");
            dispatcherTimer.Start();
            //IsEnabled should now be true after calling start
            Debug.WriteLine("dispatcherTimer.IsEnabled = " + dispatcherTimer.IsEnabled);
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            Debug.WriteLine(timesTicked + "\t time since last tick: " + span.ToString());
            timesTicked++;
        
        }
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            DispatcherTimerSetup();
        }


        void createChatsView(int howMany)
        {
            for (int i = 0; i < howMany; i++)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = "aaaaa";
                newItem.Name = "item" + i;

              //  newItem.PointerPressed += rectan1.Command;

                Windows.UI.Xaml.Media.SolidColorBrush colorForChat = new Windows.UI.Xaml.Media.SolidColorBrush();
                Windows.UI.Color actualColors = new Windows.UI.Color();
                if (i == 0)
                {
                    actualColors.R = 125;
                    actualColors.G = 255;
                    actualColors.B = 0;
                   
                }
                else
                {
                    actualColors.R = 125;
                    actualColors.G = 0;
                    actualColors.B = 255;
                }
                actualColors.A = 255;
                colorForChat.Color = actualColors;
                newItem.Background = colorForChat;

                newItem.AddHandler(PointerReleasedEvent, new PointerEventHandler(toolStripClick), true);

                chatList.Items.Add(newItem);
                Debug.WriteLine(newItem.Background);
            }
           // ((ListBox)chatList).Items
             
        }

        private void toolStripClick(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(chatWindowActivity), ((ListBoxItem)sender).Name[((ListBoxItem)sender).Name.Length-1]);
        }


        private void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("hoho " + ((Windows.UI.Xaml.Shapes.Rectangle)(sender)).Name);
            this.Frame.Navigate(typeof(chatWindowActivity), null);
        }
    }
}
