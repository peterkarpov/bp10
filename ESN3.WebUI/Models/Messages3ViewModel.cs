using ESN.Domain.Entities;
using ESN3.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class Messages3ViewModel
    {
        public Profile Profile { get; set; }

        public List<Talk3> Talks { get; set; }

    }
}