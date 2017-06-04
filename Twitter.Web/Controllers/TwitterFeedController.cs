using System.Web.Mvc;
using Twitter.Business.Services;

namespace Twitter.Web.Controllers
{
    public class TwitterFeedController : Controller
    {
        /// <summary>
        /// index method to load the page
        /// </summary>
        /// <returns></returns>
        public ActionResult index()
        {
            return View("Home");
        }

        /// <summary>
        /// Get the twitter feed details and return as string
        /// </summary>
        /// <returns></returns>
        public string GetTwitterFeedDetails()
        {           
            var twitterFeedManager = new TwitterFeedManager();
            return twitterFeedManager.GetTwitterFeedDetails();
        }
    }
}