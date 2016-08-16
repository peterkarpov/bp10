using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class FriendsListViewModel
    {
        public IEnumerable<Profile> Friends { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public User User { get; set; }

        public Profile Profile { get; set; }

    }
}