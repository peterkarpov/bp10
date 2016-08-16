using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN.WebUI.Models;

namespace ESN.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileRepository repository;
        public int pageSize = 4;

        public ProfileController(IProfileRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProfilesListViewModel model = new ProfilesListViewModel
            {
                Profiles = repository.Profiles
                    .Where(p => category == null || p.Gender == category)
                    .OrderBy(profile => profile.ProfileId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repository.Profiles.Count() :
                        repository.Profiles.Where(profile => profile.Gender == category).Count()
                },
                CurrentCategory = category,
            };
            return View(model);
        }

        public FileContentResult GetImage(Guid ProfileId)
        {
            Profile profile = repository.Profiles
                .FirstOrDefault(g => g.ProfileId == ProfileId);

            if (profile != null)
            {
                return File(profile.ImageData, profile.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        //public FileContentResult GetImage(int ProfileId)
        //{
        //    Profile profile = repository.Profiles
        //        .FirstOrDefault(p => p.ProfileId == ProfileId);

        //    if (profile != null)
        //    {
        //        return File(profile.ImageData, profile.ImageMimeType);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}