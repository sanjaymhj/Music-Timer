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

namespace Music_Timer
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }
        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton home = new ApplicationBarIconButton(new Uri("/Assets/AppBar/home1.png", UriKind.Relative));
            home.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(home);
            home.Text = "home";
            home.Click += new EventHandler(home_Click);

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
        private void home_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }

}