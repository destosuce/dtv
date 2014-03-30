using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PhoneApp2.Social
{
    public class GetGlueClient
    {
        #region properties
        public  string BaseUrlOauth2
        {
            get { return "https://api.getglue.com/oauth2/";}
        }

        public string BaseUrlApi
        {
            get { return "https://api.getglue.com/v3"; }
        }
        #endregion

        #region contructor
        public GetGlueClient(string clientId, string clientSecret)
        {
            GetGlueConfiguration.Client_Id = clientId;
            GetGlueConfiguration.Client_Secret = clientSecret;
        }
        #endregion

        #region method
        public string GetUrlAuthorize()
        {
            string sAuthorize = String.Format("{4}authorize?scope={0}&response_type={1}&redirect_uri={2}&client_id={3}&style=mobile", "public+read+write", "code", "http%3A%2F%2Fgetglue.com", GetGlueConfiguration.Client_Id, BaseUrlOauth2);
            return sAuthorize;
        }

        public string GetUrlAuthorize(string scope, string responseType, string redirectUrl, string clientId)
        {
            string sAuthorize = String.Format("{4}authorize?scope={0}&response_type={1}&redirect_uri={2}&client_id={3}&style=mobile", scope, responseType, redirectUrl, clientId, BaseUrlOauth2);
            return sAuthorize;
        }

        public string GetUrlAccessToken(string code)
        {
            string sUrlAccessToken = String.Format("{5}access_token?code={0}&grant_type={1}&client_secret={2}&redirect_uri={3}&client_id={4}", code, "authorization_code", GetGlueConfiguration.Client_Secret, "http%3A%2F%2Fgetglue.com", GetGlueConfiguration.Client_Id, BaseUrlOauth2);
            return sUrlAccessToken;
        }

        public string GetUrlAccessToken(string code, string grantType, string clientSecret, string redirectUri, string clientId)
        {
            string sUrlAccessToken = String.Format("{5}access_token?code={0}&grant_type={1}&client_secret={2}&redirect_uri={3}&client_id={4}", code, grantType, clientSecret, redirectUri, clientId, BaseUrlOauth2);
            return sUrlAccessToken;
        }


        #endregion
    }
}
