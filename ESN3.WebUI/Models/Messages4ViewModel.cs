using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class Messages4ViewModel
    {
        public Profile Profile { get; set; }

        public List<Talk> Talks { get; set; }
    }
}