using App2.Social;
using PhoneApp2.Social;
using PortableClassLibrary1.Social;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FlexibleWebAuth : Page
    {
        public EventHandler CancelledEvent { get; set; }
        public EventHandler UriChangedEvent { get; set; }

        public FlexibleWebAuth()
        {
            InitializeComponent();
            Loaded += FlexibleWebAuth_Loaded;
            wv.LoadCompleted += wv_LoadCompleted;
            wv.NavigationFailed += wv_NavigationFailed;
            wv.ManipulationStarting += wv_ManipulationStarting;
            
        }

        void wv_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            int a = 4;
            a--;
        }

        void wv_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            int a = 4;
            a--;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        async void  wv_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri != null && e.Uri.OriginalString.Contains("&code="))
            {
                int indexGet = e.Uri.OriginalString.IndexOf("&code=") + 6;
                string sCode = e.Uri.OriginalString.Substring(indexGet, e.Uri.OriginalString.Length - indexGet);
                TabletGetGlueClient client = new TabletGetGlueClient(GetGlueConfiguration.Client_Id, GetGlueConfiguration.Client_Secret);
                pbLoading.IsIndeterminate = true;
                AccessTokenResponse response = await client.GetAccessToken(sCode);
                GetGlueConfiguration.Access_Token = response.access_token;

                Popup pParent = (Popup)fAuth.Parent;
                pParent.IsOpen = false;
            }
            pbLoading.IsIndeterminate = false;
            //bad code, but 'works' for now
            if (UriChangedEvent != null)
                UriChangedEvent.Invoke(e.Uri, null);
        }

        void FlexibleWebAuth_Loaded(object sender, RoutedEventArgs e)
        {
            wv.Width = Width / 2; //while fixed, would make this work better for 'final'
        }

        public void Navigate(Uri uri)
        {
            wv.Navigate(uri);
        }

        public void Cancelled(object sender, RoutedEventArgs e)
        {
            if (CancelledEvent != null)
                CancelledEvent.Invoke(null, null);
        }
    }
}
