using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN3.WebUI.Infrastructure;
using ESN3.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public ProfileController(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        private string NavSec = "MyProfile";

        private string GetFriendStatus(Guid? ProfileId)
        {
            if (!User.Identity.IsAuthenticated) return "no identity";

            var isYourProfile = ProfileId.ToString() == User.Identity.Name.Split('|')[1];

            var isYourFriend = otherRepository.Friends.
                Where(p=>p.ProfileId.ToString()==User.Identity.Name.Split('|')[1]).
                Where(p=>p.subscriberId==ProfileId).
                FirstOrDefault()!=null;

            var isYourSubscriber = otherRepository.Friends.
                Where(p=>p.ProfileId==ProfileId).
                Where(p=>p.subscriberId.ToString() == User.Identity.Name.Split('|')[1]).
                FirstOrDefault()!=null;

            //var b = otherRepository.Friends.Contains(new Friend {ProfileId=(Guid)ProfileId, subscriberId = Guid.Parse(User.Identity.Name.Split('|')[1]) });

            var isBestFriends = isYourFriend & isYourSubscriber;

            string status = "none";

            if (isYourProfile)
            {
                status = "your profile";
            }
            else
            {
                if (isBestFriends)
                {
                    status = "your best friends";
                }
                else
                {
                    if (isYourFriend)
                    {
                        status = "your friend";
                    }

                    if (isYourSubscriber)
                    {
                        status = "your subscriber";
                    }
                }
            }

            return status;
        }

        public ActionResult Index()
        {
            ViewBag.NavSec = NavSec;
            ViewBag.NavProfileSec = "Index";

            return View();
        }

        public ActionResult ShowOne(Guid? ProfileId, string login)
        {
            ProfileViewModel model = new ProfileViewModel();

            if (login != null)
            {
                model.User = otherRepository.Users.FirstOrDefault(u => u.login == login);
            }

            if (model.User != null)
            {
                ProfileId = model.User.UserId;
            }


            
            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);
            
            ViewBag.FriendStatus = GetFriendStatus(ProfileId);

            ViewBag.NavSec = NavSec;
            ViewBag.NavProfileSec = "ShowOne";

            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(ProfileViewModel model, string returnUrl)
        {
            model.User = otherRepository.Users.FirstOrDefault(u => u.login == User.Identity.Name.Split('|')[0]);

            ViewBag.NavSec = NavSec;
            ViewBag.NavProfileSec = "Create";


            if (ModelState.IsValid)
            {
                Profile Profile = new Profile
                {
                    ProfileId = model.User.UserId,
                    fName = model.User.login,
                };

                if (repository.SaveProfile(Profile))
                {
                    return Redirect(returnUrl ?? Url.Action("Edit", "Profile", new { profile = Profile }));
                }
                else
                {
                    ModelState.AddModelError("", "data wrong");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Delete(Guid? ProfileId, string returnUrl)
        {
            var user = otherRepository.Users.FirstOrDefault(u => u.UserId == ProfileId);
            var profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            if (user.login != User.Identity.Name)
            {
                TempData["message-error"] = String.Format("you can't delete other profile");
                return RedirectToAction("Index");
            }

            if (repository.DeleteProfile(ProfileId) != null)
            {
                TempData["message-complete"] = String.Format("profile of {0} has been deleted", profile.fName);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message-error"] = String.Format("delete profile failed");
                return RedirectToAction("Index");
            }
        }

        [ProfileAuth(Order = 1)]
        [Authorize]
        public ActionResult Edit(Guid? ProfileId, string login)
        {
            if (User.Identity.Name.Split('|')[0] != login)
            {
                TempData["message-error"] = String.Format("edit profile failed");
                return RedirectToAction("Index");
            }

            if (ProfileId == null)
                ProfileId = otherRepository.Users.FirstOrDefault(u => u.login == login).UserId;

            var Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);


            ////!!!!

            //var images = from p in otherRepository.Photobooks
            //             from i in otherRepository.Images
            //             where p.ProfileId == ProfileId
            //             where i.PhotobookId == p.PhotobookId
            //             select i.ImageId;

            //ViewBag.Images = images.ToList();

            return View(Profile);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Profile profile, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (repository.SaveProfile(profile))
                {
                    TempData["message-complete"] = String.Format("update profile {0} complete", profile.fName);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message-error"] = String.Format("update {0} failure", profile.fName);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more parameters no valid");
                //return Redirect(returnUrl ?? Url.Action("Index", "Authentication"));

                return View();
            }
        }

        public ActionResult SetAvatar(Guid? ProfileId, Guid? ImageId)
        {
            var profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            profile.AvatarImageId = ImageId;

            repository.SaveProfile(profile);

            return View("Index");
        }

        public PartialViewResult ImagesForAvatar(Guid? PhotobookId)
        {
            var photobook = otherRepository.Photobooks.FirstOrDefault(p => p.PhotobookId == PhotobookId);

            var model = photobook.Images.ToList();

            return PartialView(model); //one
        }

        public ActionResult PhotobooksForAvatar(Guid? ProfileId)
        {
            var photobooks = otherRepository.Photobooks.Where(p => p.ProfileId == ProfileId).ToList();
            
            return View(photobooks);
        }

    }
}