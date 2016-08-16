using System.Collections.Generic;
using ESN.Domain.Entities;

namespace ESN.WebUI.Models
{
    public class ProfilesListViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}