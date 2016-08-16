using ESN.Domain.Abstract;
using ESN.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile

        private IProfileRepository repository;
        public int pageSize = 4;
        public ProfileController(IProfileRepository repo)
        {
            repository = repo;
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ViewResult List(string category, int page = 1)
        {
            ProfilesListViewModel model = new ProfilesListViewModel
            {
                Profiles = repository.Profiles
                    .Where(p => category == null)
                    .OrderBy(profile => profile.userId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repository.Profiles.Count() :
                        repository.Profiles.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}