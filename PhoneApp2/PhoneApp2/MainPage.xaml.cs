using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp2.Resources;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using PhoneApp2.Social;

namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            /*
                https://api.getglue.com/oauth2/authorize"
                        + "?scope=public+read+write"
                        + "&response_type=code"
                        + "&redirect_uri=http%3A%2F%2Flocalhost"
                        + "&client_id=" + CLIENT_ID
                
             */
            //https://api.getglue.com/oauth2/authorize/?scope=public+read+write&response_type=code&redirect_uri=http%3A%2F%2Flocalhost&client_id=D3DD13DD0A9385447EC629CFCF4DBF
            // lay duoc code : dfda


            /*
                ("https://api.getglue.com/oauth2/access_token"
                        + "?code=" + code
                        + "&grant_type=authorization_code"
                        + "&client_secret=" + CLIENT_SECRET
                        + "&redirect_uri=http%3A%2F%2Flocalhost"
                        + "&client_id=" + CLIENT_ID);
             */
            //https://api.getglue.com/oauth2/access_token/?code=QqUnbd8wNexjqQDad9UPjjCZFaIZu5&grant_type=authorization_code&client_secret=97EC3D64B2BD96686C6C6CDCFB2271&redirect_uri=http%3A%2F%2Flocalhost&client_id=D3DD13DD0A9385447EC629CFCF4DBF
            //lay duoc
            /*
                    {"token_type": "Bearer", "access_token": "LMvsK0Pl9khLUVg3Ww8NJBKD3Vaml6", "scope": "public read write", "expires_in": 5184000, "refresh_token": "HRliwUqbHsr5ahmqqsYDZa1scfph32"}
             */

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("http://api.getglue.com/v2");
            
            OAuth2Authenticator tt;
            tt = new OAuth2AuthorizationRequestHeaderAuthenticator("3kOuJPb1ghoZT13fI3CyA6LI6phIEc");
            //OAuth1Authenticator.ForProtectedResource("D3DD13DD0A9385447EC629CFCF4DBF", "97EC3D64B2BD96686C6C6CDCFB2271", "3kOuJPb1ghoZT13fI3CyA6LI6phIEc", "");
            var request = new RestRequest("/tv_shows/directv/checkins");
            //request.Method = Method.POST;
            
            /*var request = new RestRequest("/access_token");
            request.AddParameter("code", "NeamstwfEN0PnZr8h1DqEqvR5DTGzH", ParameterType.GetOrPost);
            request.AddParameter("grant_type", "authorization_code", ParameterType.GetOrPost);
            request.AddParameter("client_secret", "97EC3D64B2BD96686C6C6CDCFB2271", ParameterType.GetOrPost);
            request.AddParameter("redirect_uri", "http%3A%2F%2Flocalhost", ParameterType.GetOrPost);
            request.AddParameter("client_id", "D3DD13DD0A9385447EC629CFCF4DBF", ParameterType.GetOrPost);*/
            // request.AddParameter("category", "all",ParameterType.GetOrPost);
            request.AddParameter("oauth_consumer_key", "h5TITVmcvk3A7rnyDIvOXrwrsvP0Cv", ParameterType.GetOrPost);
            request.AddParameter("comment", "testingtesinttesing", ParameterType.GetOrPost);
            client.ExecuteAsync(request, response =>
            {
                int a = 4;
                a--;
            });
        }

        void GetRequestStreamCallback(IAsyncResult callbackResult)
        {

        }

        private async void btPost_Click(object sender, RoutedEventArgs e)
        {
           /* //https://api.getglue.com/v3/tv_shows/glee?access_token=3C42CA23D866B179C151BCC3474739
            var client = new RestClient("https://api.getglue.com/v3");
           // client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("3C42CA23D866B179C151BCC3474739");
            var request = new RestRequest("/tv_shows/directv/checkins");
            request.AddParameter("access_token", "3C42CA23D866B179C151BCC3474739", ParameterType.GetOrPost);
            request.AddParameter("comment", "daylacommentcuatoi", ParameterType.GetOrPost);
            request.AddParameter("action", "share", ParameterType.GetOrPost);
            request.Method = Method.POST;
            client.ExecuteAsync(request, response => {
                int a = 4;
                a--;
            });   
            */

            MobileGetGlueClient mobileGetGlueClient = new MobileGetGlueClient(GetGlueConfiguration.Client_Id, GetGlueConfiguration.Client_Secret);
            //await mobileGetGlueClient.GetAccessToken("ej1KsNIo1gD9ROVc5SbEPvSnoJuYdU");
            //await mobileGetGlueClient.Checkin("/tv_shows/directv", "ADF2457988C96D5C363A3559D92ADB", "testing thoi nha <3");
            mobileGetGlueClient.Authorize();
            
            int b = 4;
            b--;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}