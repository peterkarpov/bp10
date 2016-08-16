using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class Talk1
    {
        public Profile to { get; set; }

        public Profile from { get; set; }

        public List<Message> Messages { get; set; }
    }
}