using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Twitter.Business.Interfaces;
using Twitter.Model;

namespace Twitter.Business.Services
{
    /// <summary>
    /// Twitter authentication class, which includes the authentication methods
    /// </summary>
    public class TwitterFeedAuth : ITwitterFeedAuth
    {
        /// <summary>
        /// This method is used to get the authentication token based on the consumer key and consumer Secret key
        /// </summary>
        /// <param name="authDetails"></param>
        /// <returns>AuthResponse</returns>
        public AuthResponse GetAuthenticateTockenDetails(AuthDetails authDetails)
        {
            if (authDetails == null)
                return null;
            else if (string.IsNullOrEmpty(authDetails.AuthURL) || string.IsNullOrEmpty(authDetails.ConsumerKey) || string.IsNullOrEmpty(authDetails.ConsumerSecretKey))
                return null;           

            AuthResponse authResponse = new AuthResponse();
            
            var authHeaderFormat = "Basic {0}";

            var authHeader = string.Format(authHeaderFormat,
                                           Convert.ToBase64String(
                                               Encoding.UTF8.GetBytes(Uri.EscapeDataString(authDetails.ConsumerKey) + ":" +

                                                                      Uri.EscapeDataString((authDetails.ConsumerSecretKey)))

                                               ));
            
            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(authDetails.AuthURL);

            //Add header information as per detail provided in twitter api
            authRequest.Headers.Add("Authorization", authHeader);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            
            var postBody = "grant_type=client_credentials";
            using (Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }
            authRequest.Headers.Add("Accept-Encoding", "gzip");
            WebResponse twitAuthResponse = authRequest.GetResponse();           
            using (twitAuthResponse)
            {
                using (var reader = new StreamReader(twitAuthResponse.GetResponseStream()))
                {
                    string objectText = reader.ReadToEnd();
                    if (objectText != "" || objectText != null)
                    {                       
                        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                        var authToken = json_serializer.Deserialize<Dictionary<string, string>>(objectText);
                        if (authToken != null)
                        {
                            authResponse.TokenType = authToken["token_type"];
                            authResponse.Accesstoken = authToken["access_token"];
                        }
                    }

                }
            }

            return authResponse;
        }
    }
}
