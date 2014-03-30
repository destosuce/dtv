using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using PortableClassLibrary1.Social;
using RestSharp.Serializers;
using RestSharp.Deserializers;
using PhoneApp2.Views;
using Microsoft.Phone.Controls;
using System.Net;


namespace PhoneApp2.Social
{
    public class MobileGetGlueClient : GetGlueClient
    {
        public MobileGetGlueClient(string clientId, string clientSecret) : base(clientId, clientSecret)
        { 
        
        }

        public async Task<AccessTokenResponse> GetAccessToken(string code)
        {
            AccessTokenResponse accessTokenData = new AccessTokenResponse();

            var client = new RestClient("https://api.getglue.com/oauth2");
            var request = new RestRequest("/access_token");
             request.AddParameter("code", code, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "authorization_code", ParameterType.GetOrPost);
            request.AddParameter("client_secret", GetGlueConfiguration.Client_Secret, ParameterType.GetOrPost);
            request.AddParameter("redirect_uri", "http%3A%2F%2Fgetglue.com", ParameterType.GetOrPost);
            request.AddParameter("client_id", GetGlueConfiguration.Client_Id, ParameterType.GetOrPost);

             IRestResponse iRestResponse = await client.ExecuteAwait(request);
             if (!String.IsNullOrEmpty(iRestResponse.Content) && iRestResponse.Content.Contains("access_token"))
             {
                 JsonDeserializer jsonDeserializer = new JsonDeserializer();
                 accessTokenData = jsonDeserializer.Deserialize<AccessTokenResponse>(iRestResponse);
                 GetGlueConfiguration.Access_Token = accessTokenData.access_token;
             }
            return accessTokenData;
        }

        public void Authorize()
        {
            string urlAuthorize = this.GetUrlAuthorize();

            PhoneApplicationPage pageCurrent = (PhoneApplicationPage)App.RootFrame.Content;
            string pathNavigate = String.Format("/Views/GetGlueLoginPage.xaml?path={0}", HttpUtility.UrlEncode(urlAuthorize));
            App.RootFrame.Navigate(new Uri(pathNavigate, UriKind.Relative));
            
        }

        public async Task<IRestResponse> Checkin(string objectId, string accessToken, string comment)
        {

            if (String.IsNullOrEmpty(objectId))
            { 
                char cEndChar = objectId[objectId.Length - 1];
                if (cEndChar.Equals('/'))
                    objectId.Remove(objectId.Length - 1, 1);
            }

            String pathQuery = String.Format("{0}/checkins", objectId);
            var request = new RestRequest(pathQuery);
            request.AddParameter("access_token",accessToken, ParameterType.GetOrPost);
            request.AddParameter("comment", comment, ParameterType.GetOrPost);
            request.AddParameter("action", "share", ParameterType.GetOrPost);

            var client = new RestClient(BaseUrlApi);
            IRestResponse response = await client.ExecuteAwait(request);

            return response;
        }

        public async Task<IRestResponse> Checkin(string objectId, string comment)
        {

            if (String.IsNullOrEmpty(objectId))
            {
                char cEndChar = objectId[objectId.Length - 1];
                if (cEndChar.Equals('/'))
                    objectId.Remove(objectId.Length - 1, 1);
            }

            String pathQuery = String.Format("{0}/checkins", objectId);
            var request = new RestRequest(pathQuery);
            request.AddParameter("access_token", GetGlueConfiguration.Access_Token, ParameterType.GetOrPost);
            request.AddParameter("comment", comment, ParameterType.GetOrPost);
            request.AddParameter("action", "share", ParameterType.GetOrPost);

            var client = new RestClient(BaseUrlApi);
            IRestResponse response = await client.ExecuteAwait(request);

            return response;
        }
    }
}
