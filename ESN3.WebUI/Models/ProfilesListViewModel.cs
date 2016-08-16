﻿using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public class ProfilesListViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public Profile Profile { get; set; }

        public Profile searchProfile { get; set; }

        public SearchProfileListViewModel sModel { get; set; }

    }
}