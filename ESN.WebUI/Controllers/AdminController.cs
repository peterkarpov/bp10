using System.Web.Mvc;
using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using System.Linq;
using System;
using System.Web;

namespace ESN.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProfileRepository repository;

        public AdminController(IProfileRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Profiles);
        }

        public ViewResult Edit(Guid ProfileId)
        {
            Profile profile = repository.Profiles
                .FirstOrDefault(g => g.ProfileId == ProfileId);
            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(Profile profile, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    profile.ImageMimeType = image.ContentType;
                    profile.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(profile.ImageData, 0, image.ContentLength);
                }
                repository.SaveProfile(profile);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", profile.fName);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(profile);
            }
        }

        //[HttpPost]
        //public ActionResult Edit(Profile profile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.SaveProfile(profile);
        //        TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", profile.fName);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // Что-то не так со значениями данных
        //        return View(profile);
        //    }
        //}

        public ViewResult Create()
        {
            return View("Edit", new Profile());
        }

        [HttpPost]
        public ActionResult Delete(Guid ProfileId)
        {
            Profile deletedProfile = repository.DeleteProfile(ProfileId);
            if (deletedProfile != null)
            {
                TempData["message"] = string.Format("Профиль \"{0}\" был удален", deletedProfile.fName);
            }
            return RedirectToAction("Index");
        }

    }
}