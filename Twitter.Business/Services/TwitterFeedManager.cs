using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Interfaces;
using Twitter.Model;

namespace Twitter.Business.Services
{
    public class TwitterFeedManager : ITwitterFeedManager
    {
        /// <summary>
        /// Authenticate and fetch the recent tweets from the Twitter API
        /// </summary>
        /// <returns></returns>
        public string GetTwitterFeedDetails()
        {
            try
            {
                // get the details from the config file
                var authDetails = new AuthDetails
                {
                    ConsumerKey = ConfigurationManager.AppSettings["authConsumerKey"],
                    ConsumerSecretKey = ConfigurationManager.AppSettings["authConsumerSecret"],
                    AuthURL = ConfigurationManager.AppSettings["authUrl"]
                };

                string timelineFormat = ConfigurationManager.AppSettings["twitterURLFormat"];
                string screenname = ConfigurationManager.AppSettings["screenname"];
                int count = Convert.ToInt16(ConfigurationManager.AppSettings["count"]);
                string include_rts = ConfigurationManager.AppSettings["IncludeRts"];
                string exclude_replies = ConfigurationManager.AppSettings["ExcludeReplies"];

                string tweetDetails = string.Empty;

                // Get Authentication token details
                ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
                AuthResponse twitAuthResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

                //update the URL with Parameters.
                string TimelineUrl = string.Format(timelineFormat, screenname, include_rts, exclude_replies, count);

                // Get Twitter feed in Json format.
                if (twitAuthResponse != null)
                {                 
                    if (string.IsNullOrEmpty(TimelineUrl) || string.IsNullOrEmpty(twitAuthResponse.TokenType) || string.IsNullOrEmpty(twitAuthResponse.Accesstoken))
                        return tweetDetails;

                    HttpWebRequest apiRequest = (HttpWebRequest)WebRequest.Create(TimelineUrl);
                   
                    var timelineHeaderFormat = "{0} {1}";
                    apiRequest.Headers.Add("Authorization",
                                                string.Format(timelineHeaderFormat, twitAuthResponse.TokenType,
                                                              twitAuthResponse.Accesstoken));
                    apiRequest.Method = "Get";
                  
                    WebResponse timeLineResponse = apiRequest.GetResponse();

                    using (timeLineResponse)
                    {
                        using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                        {
                            tweetDetails = reader.ReadToEnd();
                        }
                    }
                    return tweetDetails;
                }

                return tweetDetails;
            }
            catch(Exception e)
            {
                //Catch the exception
                throw e;
            }
        }
    }
}
