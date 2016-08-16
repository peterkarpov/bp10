using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class TalkViewModel
    {
        public Guid? TalkId { get; set; }

        public Profile from { get; set; }

        public Profile to { get; set; }

        public List<Message> Messages { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public int pageSize { get; set; }
    }
}