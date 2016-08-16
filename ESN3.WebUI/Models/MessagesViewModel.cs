using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class MessagesViewModel
    {
        public List<Talk1> Talks { get; set; }

        //public List<Message> Messages { get; set; }

        public User User { get; set; }
        public Profile Profile { get; set; }
    }
}