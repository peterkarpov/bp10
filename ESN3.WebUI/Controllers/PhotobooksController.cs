using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN3.WebUI.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class PhotobooksController : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public PhotobooksController(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        private string NavSec = "Photobooks";

        //[Inject]
        //public IOtherRepository Db { get; set; }

        public ActionResult Index(string login)
        {
            PhotobooksViewModel model = new PhotobooksViewModel();

            User User = default(User);
            model.Profile = default(Profile);
            model.Photobooks = new List<Photobook>();

            if (login != null)
                User = otherRepository.Users.FirstOrDefault(u => u.login == login);

            if (User != default(User))
                model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == User.UserId);

            if (model.Profile != default(Profile))
                model.Photobooks = otherRepository.Photobooks.Where(p => p.ProfileId == model.Profile.ProfileId).ToList();

            ViewBag.NavSec = NavSec;

            return View(model);

        }

        public ActionResult All()
        {
            PhotobooksViewModel model = new PhotobooksViewModel();

            model.Photobooks = new List<Photobook>();

            if (model.Profile == default(Profile))
                model.Photobooks = otherRepository.Photobooks.ToList();

            ViewBag.NavSec = NavSec;

            return View(model);
        }

        public ViewResult Edit(string photobookId)
        {
            Photobook photobook = otherRepository.Photobooks
                .FirstOrDefault(p => p.PhotobookId == Guid.Parse(photobookId));
            return View(photobook);
        }

        [HttpPost]
        public ActionResult Edit(Photobook photobook)
        {
            if (ModelState.IsValid)
            {
                if (photobook != null)
                {
                    
                }

                photobook.ProfileId = otherRepository.Users.FirstOrDefault(u => u.login == User.Identity.Name).UserId;

                otherRepository.SavePhotobook(photobook);
                TempData["message"] = string.Format("Save of \"{0}\" is complete", photobook.Title);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(photobook);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Photobook());
        }

        public ViewResult ConcretePhotobook(string PhotebookId)
        {
            PhotobooksViewModel model = new PhotobooksViewModel();

            model.Photebook = otherRepository.Photobooks.FirstOrDefault(p => p.PhotobookId == Guid.Parse(PhotebookId));

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == model.Photebook.ProfileId);

            model.Images = otherRepository.Images.Where(i => i.PhotobookId == model.Photebook.PhotobookId).ToList();

            return View(model);
        }

    }
}