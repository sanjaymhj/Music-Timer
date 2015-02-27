using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Music_Timer.Resources;
using System.Windows.Threading;
using Microsoft.Xna.Framework.Media;
using Microsoft.Phone.Tasks;

namespace Music_Timer
{
    public partial class MainPage : PhoneApplicationPage
    {
        //Variables
        bool timer_set = false;// set_again= false;
        Int16 dat_min, dat_sec, dat_hrs, dat_day;
        bool bol_start;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            start_timer.IsEnabled = true;
            stop_timer.IsEnabled = false;
            default_start();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();
        }

        private void default_start()
        {
            bol_start = true;
            Double sec, min, hrs, day;
            sec = Convert.ToInt16(DateTime.Now.Second);
            min = Convert.ToInt16(DateTime.Now.Minute);
            hrs = Convert.ToInt16(DateTime.Now.Hour);
            day = Convert.ToInt16(DateTime.Now.Day);
            txtClock.Text = "Start Time - " + hrs + " : " + min + " : " + sec;
            if (enter_second.Text == "")
            {
                enter_second.Text = "0";
            }
            else if (Convert.ToInt16(enter_second.Text) >= 60)
            {
                MessageBox.Show("UpperLimit is 60");
                bol_start = false;
            }
            if (enter_minute.Text == "")
            {
                enter_minute.Text = "0";
            }
            else if (Convert.ToInt16(enter_minute.Text) >= 60)
            {
                MessageBox.Show("UpperLimit is 60");
                bol_start = false;
            }
            if (enter_hour.Text == "")
            {
                enter_hour.Text = "0";
            }
            else if (Convert.ToInt16(enter_hour.Text) >= 12)
            {
                MessageBox.Show("UpperLimit is 12");
                bol_start = false;
            }
            if (enter_second.Text == "0" && enter_minute.Text == "0" && enter_hour.Text == "0")
            {
                enter_minute.Text = "14";
            }
            if (bol_start)
            {
                start_timer.IsEnabled = false;
                stop_timer.IsEnabled = true;
                timecapture();
            }
        }

        private void start_timer_Click(object sender, RoutedEventArgs e)
        {
            default_start();
            start_timer.IsEnabled = false;
            stop_timer.IsEnabled = true;
        }


        private void timecapture()
        {
            DispatcherTimer newTimer = new DispatcherTimer();

            dat_sec = Convert.ToInt16(DateTime.Now.Second);
            dat_min = Convert.ToInt16(DateTime.Now.Minute);
            dat_hrs = Convert.ToInt16(DateTime.Now.Hour);
            dat_day = Convert.ToInt16(DateTime.Now.Day);
            
            dat_sec += Convert.ToInt16(enter_second.Text);
            dat_min += Convert.ToInt16(enter_minute.Text);
            dat_hrs += Convert.ToInt16(enter_hour.Text);
            double dat_min1, dat_sec1, dat_hrs1, dat_day1;
            dat_sec1 = dat_sec;
            dat_min1 = dat_min;
            dat_hrs1 = dat_hrs;
            dat_day1 = dat_day;
            if(dat_sec1>=60)
            {
                dat_min1=dat_min1+dat_sec1/60;
                dat_sec1=dat_sec1 % 60;
            }
            if(dat_min1>=60)
            {
                dat_hrs1 = dat_hrs1 + dat_min1 / 60;
                dat_min1 = dat_min1 % 60;
            }
            if(dat_hrs1>=24)
            {
                dat_day1 = dat_day1 + dat_hrs / 24;
                dat_hrs1 = dat_hrs % 24;
            }
            dat_sec = Convert.ToInt16(dat_sec1);
            dat_min = Convert.ToInt16(dat_min1);
            dat_hrs = Convert.ToInt16(dat_hrs1);
            dat_day = Convert.ToInt16(dat_day1);

            txtptime.Text = "Pausing Time : "+ dat_hrs + " : " + dat_min + " : " + dat_sec;
            timer_set = true;
            newTimer.Tick += OnTimerTick;
            newTimer.Start();
            
         }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Double sec, min, hrs, day;
            sec = Convert.ToInt16(DateTime.Now.Second);
            min = Convert.ToInt16(DateTime.Now.Minute);
            hrs = Convert.ToInt16(DateTime.Now.Hour);
            day = Convert.ToInt16(DateTime.Now.Day);
            txtcrtime.Text = "Current Time - " + hrs + " : " + min + " : " + sec;
            
            if (timer_set == true)
            {
                
                if (dat_sec == Convert.ToInt16(DateTime.Now.Second) && dat_min == Convert.ToInt16(DateTime.Now.Minute) && dat_hrs == Convert.ToInt16(DateTime.Now.Hour) && dat_day == Convert.ToInt16(DateTime.Today.Day))
                {
                    pausing();
                    txtptime.Text = "Paused Time : " + dat_hrs + " : " + dat_min + " : " + dat_sec;
                    MessageBox.Show("Music Paused.");//the music stop code here
                    timer_set = false;
                    bol_start = false;
                    
                }
            }
        }

        private void pausing()
        {
            MediaPlayer.Pause();
            start_timer.IsEnabled = true;
            stop_timer.IsEnabled = false;

        }

        private void stop_timer_Click(object sender, RoutedEventArgs e)
        {
            if(timer_set==true)
            {
                txtClock.Text = "";
                txtptime.Text = "";
                timer_set = false;
                start_timer.IsEnabled = true;
                stop_timer.IsEnabled = false;

            }
            else
            {
                MessageBox.Show("Start the Timer first.");
            }
            
        }

      
       

       
        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
           ApplicationBar = new ApplicationBar();

           // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton faq = new ApplicationBarIconButton(new Uri("/Assets/AppBar/faq.png", UriKind.Relative));
            faq.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(faq);
            faq.Text = "FAQ";
            faq.Click += new EventHandler(faq_Click);

            ApplicationBarIconButton about = new ApplicationBarIconButton(new Uri("/Assets/AppBar/about.png", UriKind.Relative));
            about.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(about);
            about.Text = "About";
            about.Click += new EventHandler(about_Click);

            ApplicationBarIconButton social = new ApplicationBarIconButton(new Uri("/Assets/AppBar/social.png", UriKind.Relative));
            social.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(social);
            social.Text = "social";
            social.Click += new EventHandler(social_Click);
            // Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void social_Click(object sender, EventArgs e)
        {
           // MarketplaceReviewTask rev = new MarketplaceReviewTask();
            //rev.Show();
            MessageBoxResult m = MessageBox.Show("Like us on Facebook.", "Spread the word.", MessageBoxButton.OK);
            if (m == MessageBoxResult.OK)
            {
                 app_page();
            }
            else
            {
               MessageBox.Show("We respect you descision.");
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Music Timer 1.0 by Sanjay Maharjan.", "About", MessageBoxButton.OK);
        }
        private async void app_page()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/musictimer", UriKind.Absolute));
        }
        private void faq_Click(object sender, EventArgs e)
        {
            timer_set = false;
            NavigationService.Navigate(new Uri("/FAQ.xaml", UriKind.Relative));
        }
    }
}