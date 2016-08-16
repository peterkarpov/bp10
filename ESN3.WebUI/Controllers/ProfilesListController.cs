using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN3.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class ProfilesListController : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;
        private int pageSize = 4;

        public ProfilesListController(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        private string NavSec = "Profiles";

        public ActionResult Index(int page = 1)
        {
            ProfilesListViewModel model = new ProfilesListViewModel
            {
                Profiles = repository.Profiles
                    .OrderBy(profile => profile.fName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Profiles.Count(),
                },
                searchProfile = new Profile(),
            };

            ViewBag.Total = repository.Profiles.Count();
            ViewBag.NavSec = NavSec;

            return View(model);
        }

        [HttpGet]
        public ActionResult Filter(Profile searchProfile = default(Profile), int page = 1)
        {
            var profiles = repository.Profiles;

            if (searchProfile.fName != null) profiles = profiles.Where(f => f.fName != null ? f.fName.Contains(searchProfile.fName) : false);
            if (searchProfile.lName != null) profiles = profiles.Where(l => l.lName != null ? l.lName.Contains(searchProfile.lName) : false);
            if (searchProfile.mName != null) profiles = profiles.Where(m => m.mName != null ? m.mName.Contains(searchProfile.mName) : false);

            if (searchProfile.dob != null) profiles = profiles.Where(d => d.dob != null ? d.dob > searchProfile.dob : false);
            //добавить больше\меньше и вообще разбить на даты

            if (searchProfile.Gender != null) profiles = profiles.Where(g => g.Gender == searchProfile.Gender);


            ProfilesListViewModel model = new ProfilesListViewModel
            {
                searchProfile = searchProfile,

                Profiles = profiles
                    .OrderBy(profile => profile.fName)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = profiles.Count(),
                },
            };

            ViewBag.Total = repository.Profiles.Count();
            ViewBag.NavSec = NavSec;

            return View("Index", model);
        }



    }
}