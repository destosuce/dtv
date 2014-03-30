using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp2.Social;
using PortableClassLibrary1.Social;

namespace PhoneApp2.Views
{
    public partial class GetGlueLoginPage : PhoneApplicationPage
    {

        public GetGlueLoginPage()
        {
            InitializeComponent();
            this.Loaded += GetGlueLoginPage_Loaded;
        }

        void GetGlueLoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            wbLogin.Navigated += wbLogin_Navigated;
            wbLogin.NavigationFailed += wbLogin_NavigationFailed;
            wbLogin.Navigating += wbLogin_Navigating;
        }

        public void NavigateToAuthorize(string path)
        {
            wbLogin.Navigate(new Uri(path, UriKind.Absolute));

        }

        void wbLogin_Navigating(object sender, NavigatingEventArgs e)
        {
            pbLoading.IsIndeterminate = true;
        }

        void wbLogin_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            
        }

        

        async void wbLogin_Navigated(object sender, NavigationEventArgs e)
        {
            pbLoading.IsIndeterminate = false;

            if (e.Uri.OriginalString.Contains("&code="))
            {
                int indexGet = e.Uri.OriginalString.IndexOf("&code=") + 6;
                string sCode = e.Uri.OriginalString.Substring(indexGet, e.Uri.OriginalString.Length - indexGet);
                MobileGetGlueClient client = new MobileGetGlueClient(GetGlueConfiguration.Client_Id, GetGlueConfiguration.Client_Secret);
                AccessTokenResponse response = await client.GetAccessToken(sCode);
                GetGlueConfiguration.Access_Token = response.access_token;

                this.NavigationService.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext != null && NavigationContext.QueryString.ContainsKey("path"))
            {
                string pathNavigate = NavigationContext.QueryString["path"];
                wbLogin.Navigate(new Uri(pathNavigate, UriKind.Absolute));
            }
            base.OnNavigatedTo(e);
        }
    }
}