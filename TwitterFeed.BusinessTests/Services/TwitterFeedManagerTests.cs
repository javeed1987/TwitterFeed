using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitter.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Interfaces;

namespace Twitter.Business.Services.Tests
{
    [TestClass()]
    public class TwitterFeedManagerTests
    {
        [TestMethod()]
        public void GetTwitterFeedDetailsTest()
        {
            ITwitterFeedManager _TwitterFeedManager = new TwitterFeedManager();
            var twitterFeedDetails = _TwitterFeedManager.GetTwitterFeedDetails();
            Assert.IsNotNull(twitterFeedDetails);
        }      
    }
}