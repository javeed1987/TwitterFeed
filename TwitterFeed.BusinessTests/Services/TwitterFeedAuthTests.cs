using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitter.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Model;
using System.Configuration;
using Twitter.Business.Interfaces;

namespace Twitter.Business.Services.Tests
{
    [TestClass()]
    public class TwitterFeedAuthTests
    {
        /// <summary>
        /// Positive Test
        /// </summary>
        [TestMethod()]
        public void GetAuthenticateTockenDetailsTest()
        {
            AuthDetails authDetails = new AuthDetails()
            {
                ConsumerKey = ConfigurationManager.AppSettings["authConsumerKey"],
                ConsumerSecretKey = ConfigurationManager.AppSettings["authConsumerSecret"],
                AuthURL = ConfigurationManager.AppSettings["authUrl"]
            };

            ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
            var authResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

            Assert.IsNotNull(authDetails);
            Assert.IsTrue(authResponse.TokenType != null && authResponse.Accesstoken != null);
        }


        /// <summary>
        /// Null AuthDetails object test
        /// </summary>
        [TestMethod()]
        public void GetAuthenticateTockenDetailsTestAuthDetailsNull()
        {
            AuthDetails authDetails = null;           

            ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
            var authResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

            Assert.AreEqual(authResponse, null);
        }

        /// <summary>
        /// ConsumerKey Null Test
        /// </summary>
        [TestMethod()]
        public void GetAuthenticateTockenDetailsTestConsumerKeyNull()
        {
            AuthDetails authDetails = new AuthDetails()
            {
                ConsumerKey = null,
                ConsumerSecretKey = ConfigurationManager.AppSettings["authConsumerSecret"],
                AuthURL = ConfigurationManager.AppSettings["authUrl"]
            };

            ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
            var authResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

            Assert.AreEqual(authResponse, null);           
        }

        /// <summary>
        /// ConsumerSecretKey Null Test
        /// </summary>
        [TestMethod()]
        public void GetAuthenticateTockenDetailsTestConsumerSecretKeyNull()
        {
            AuthDetails authDetails = new AuthDetails()
            {
                ConsumerKey = ConfigurationManager.AppSettings["authConsumerKey"],
                ConsumerSecretKey = null,
                AuthURL = ConfigurationManager.AppSettings["authUrl"]
            };

            ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
            var authResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

            Assert.AreEqual(authResponse, null);
        }

        /// <summary>
        /// Auth URL Null Test
        /// </summary>
        [TestMethod()]
        public void GetAuthenticateTockenDetailsTestAuthURLNull()
        {
            AuthDetails authDetails = new AuthDetails()
            {
                ConsumerKey = ConfigurationManager.AppSettings["authConsumerKey"],
                ConsumerSecretKey = null,
                AuthURL = ConfigurationManager.AppSettings["authUrl"]
            };

            ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
            var authResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

            Assert.AreEqual(authResponse, null);
        }

        /// <summary>
        /// internal server error exception test case
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
         "internal server error.")]
        public void NullUserIdInConstructor()
        {
            AuthDetails authDetails = new AuthDetails()
            {
                ConsumerKey = ConfigurationManager.AppSettings["authConsumerKey"],
                ConsumerSecretKey = ConfigurationManager.AppSettings["authConsumerSecret"],
                AuthURL = ConfigurationManager.AppSettings["authUrl"]
            };

            ITwitterFeedAuth _twitterFeedAuth = new TwitterFeedAuth();
            var authResponse = _twitterFeedAuth.GetAuthenticateTockenDetails(authDetails);

        }

    }
}