using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN.WebUI.Models
{
    public class ProfilesListViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}