using App2.Views;
using PhoneApp2.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace App2.Social
{
    public static class FlexibleWebAuthenticationBroker
    {
        public static async Task<FlexibleWebAuthenticationResult> AuthenticateAsync(WebAuthenticationOptions options, Uri startUri, Uri endUri)
        {
            FlexibleWebAuthenticationResult faResult = new FlexibleWebAuthenticationResult();
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            int result = -1;
            try
            {
                Popup p = new Popup
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = Window.Current.Bounds.Width,
                    Height = Window.Current.Bounds.Height
                };

                var f = new FlexibleWebAuth
                {
                    Width = Window.Current.Bounds.Width,
                    Height = Window.Current.Bounds.Height
                };

                f.CancelledEvent += (s, e) =>
                {
                    tcs.TrySetResult(1);
                    p.IsOpen = false;
                };

                p.Child = f;
                p.IsOpen = true;
                f.Navigate(startUri);
                result = await tcs.Task;

                faResult.ResponseStatus = WebAuthenticationStatus.Success;
                faResult.ResponseData = GetGlueConfiguration.Access_Token;
                p.IsOpen = false;
            }
            catch(Exception ex)
            {
                faResult.ResponseData = ex.Message;
                faResult.ResponseStatus = WebAuthenticationStatus.ErrorHttp;
            }

            
            if (result != 1)
            {
                faResult.ResponseStatus = WebAuthenticationStatus.ErrorHttp;
            }

            return faResult;
        }
    }

    public class FlexibleWebAuthenticationResult
    {
        public string ResponseData { get; set; }
        public WebAuthenticationStatus ResponseStatus { get; set; }
    }
}
