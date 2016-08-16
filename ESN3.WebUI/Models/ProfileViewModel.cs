using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class ProfileViewModel
    {

        public Profile Profile { get; set; }
        public User User { get; set; }
    }
}