using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class PhotobooksViewModel
    {
        public Profile Profile { get; set; }
        public List<Photobook> Photobooks { get; set; }

        public List<Image> Images { get; set; }

        public Photobook Photebook { get; set; }
        
    }
}