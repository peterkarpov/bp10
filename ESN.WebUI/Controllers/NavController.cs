using ESN.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProfileRepository repository;

        public NavController(IProfileRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Profiles
                .Select(profile => profile.Gender)
                .Where(n => !string.IsNullOrEmpty(n))
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}