using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GetGlueEngine.Social
{
    public class TabletGetGlueClient : GetGlueClient
    {
        public TabletGetGlueClient(string clientId, string clientSecret)
            : base(clientId, clientSecret)
        {

        }

        public async Task<bool> Authorize()
        {
            string goodreadsURL = this.GetUrlAuthorize();
            //string goodreadsURL = "http://getglue.com/oauth/login?style=mobile&client_id=D3DD13DD0A9385447EC629CFCF4DBF";
            //string goodreadsURL = "https://getglue.com/oauth/authorize?oauth_token=fdfas&style=mobile";

            //http://getglue.com/oauth/login?style=mobile
            //WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());

            var result = await FlexibleWebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
                return true;

            return false;
        }

        public async Task<AccessTokenResponse> GetAccessToken(string code)
        {
            AccessTokenResponse accessTokenData = new AccessTokenResponse();

            var client = new RestClient("https://api.getglue.com/oauth2/access_token");
            var request = new RestRequest("");
            request.AddParameter("code", code, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "authorization_code", ParameterType.GetOrPost);
            request.AddParameter("client_secret", GetGlueConfiguration.Client_Secret, ParameterType.GetOrPost);
            request.AddParameter("redirect_uri", "http%3A%2F%2Fgetglue.com", ParameterType.GetOrPost);
            request.AddParameter("client_id", GetGlueConfiguration.Client_Id, ParameterType.GetOrPost);

            IRestResponse iRestResponse = await client.Execute(request);
            string content = System.Text.Encoding.UTF8.GetString(iRestResponse.RawBytes, 0, iRestResponse.RawBytes.Length);

            if (!String.IsNullOrEmpty(content) && content.Contains("access_token"))
            {
                JsonDeserializer jsonDeserializer = new JsonDeserializer();
                accessTokenData = jsonDeserializer.Deserialize<AccessTokenResponse>(iRestResponse);
                GetGlueConfiguration.Access_Token = accessTokenData.access_token;
            }
            return accessTokenData;
        }

        public async Task<string> CheckIn(string objectId, string accessToken, string comment)
        {
            if (String.IsNullOrEmpty(objectId))
            {
                char cEndChar = objectId[objectId.Length - 1];
                if (cEndChar.Equals('/'))
                    objectId.Remove(objectId.Length - 1, 1);
            }

            String pathQuery = String.Format("{0}/checkins", objectId);
            var request = new RestRequest("");
            request.AddParameter("access_token", accessToken, ParameterType.GetOrPost);
            request.AddParameter("comment", comment, ParameterType.GetOrPost);
            request.AddParameter("action", "share", ParameterType.GetOrPost);

            var client = new RestClient(BaseUrlApi + pathQuery);
            IRestResponse response = await client.Execute(request);

            string content = System.Text.Encoding.UTF8.GetString(response.RawBytes, 0, response.RawBytes.Length);

            return content;
        }
    }
}
