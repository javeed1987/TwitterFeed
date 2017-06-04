using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Model;

namespace Twitter.Business.Interfaces
{
    public interface ITwitterFeedAuth
    {
        AuthResponse GetAuthenticateTockenDetails(AuthDetails authDetails);
    }
}
