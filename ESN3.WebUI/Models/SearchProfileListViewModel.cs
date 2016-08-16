using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class SearchProfileListViewModel
    {
        public string fName { get; set; }
        public string mName { get; set; }
        public string lName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime younger { get; set; }
        public DateTime upward { get; set; }

        public DateTime year { get; set; }
        public DateTime month { get; set; }
        public DateTime day { get; set; }

        
        public bool searchOnFriends { get; set; }
    }
}