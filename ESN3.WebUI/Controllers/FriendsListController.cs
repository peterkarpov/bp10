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
    public class FriendsListController : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        private int PageSize = 4;

        public FriendsListController(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        private string NavSec = "Friends";

        private Profile GetProfileByLogin(string login)
        {
            Profile profile = null;

            if (!String.IsNullOrEmpty(login))
            {
                var user = otherRepository.Users.FirstOrDefault(u => u.login == login);

                if (user != null)
                {
                    profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == user.UserId);
                }
            }
            
            return profile;
        }

        [Authorize]
        [ProfileAuth(Order = 1)]
        public ActionResult FriendsOfUser(Guid? ProfileId, string login, int page=1)
        {
            //FriendsListViewModel model = new FriendsListViewModel();

            //if (login != null)
            //    model.User = otherRepository.Users.FirstOrDefault(u => u.login == login);


            //if (model.User != null)
            //{
            //    List<Guid> FriendsId = otherRepository.Friends.Where(f => f.ProfileId == model.User.UserId).Select(x => x.ProfileId).ToList();
            //    model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == model.User.UserId);
            //    model.Friends = repository.Profiles.Where(u => FriendsId.Contains(u.ProfileId)).ToList();
            //}

            //model.PagingInfo = new PagingInfo
            //{
            //    CurrentPage = page,
            //    ItemsPerPage = PageSize,
            //    TotalItems = model.Friends == null ? 0 : model.Friends.Count(),
            //};

            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }

            ProfilesListViewModel model = new ProfilesListViewModel();

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            var friendsOfProfile = from f in otherRepository.Friends
                                   where f.ProfileId == ProfileId
                                   join p in repository.Profiles on f.subscriberId equals p.ProfileId
                                   select p;

            model.Profiles = friendsOfProfile
                .OrderBy(p => p.fName)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = friendsOfProfile.Count(),
            };

            ViewBag.TotalItems = model.PagingInfo.TotalItems;
            ViewBag.NavSec = NavSec;

            return View(model);
        }

        [Authorize]
        public ActionResult SubscriberOfUser(Guid? ProfileId, string login, int page = 1)
        {
            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }

            ProfilesListViewModel model = new ProfilesListViewModel();

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            var subscribersOfUser = from s in otherRepository.Friends
                                      where s.subscriberId == ProfileId
                                      join p in repository.Profiles on s.ProfileId equals p.ProfileId
                                      select p;

            model.Profiles = subscribersOfUser
                .OrderBy(p => p.fName)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = subscribersOfUser.Count(),
            };

            ViewBag.TotalItems = model.PagingInfo.TotalItems;
            ViewBag.NavSec = NavSec;

            return View(model);
        }

        [Authorize]
        public ActionResult Index()
        {

            ViewBag.NavSec = NavSec;

            return View();
        }

        [Authorize]
        [ProfileAuth(Order=1)]
        public ActionResult AddToFriends(Guid? ProfileId, Guid? subscriberId, string returnUrl, string login = null, string subscriberlogin = null)
        {
            if (ProfileId == null)
            {
                var Profile = GetProfileByLogin(login);
                ProfileId = Profile.ProfileId;
            }

            if (subscriberId == null)
            {
                var subscriber = GetProfileByLogin(login);
                subscriberId = subscriber.ProfileId;
            }

            var friends = new Friend()
            {
                ProfileId = (Guid)ProfileId,
                subscriberId = (Guid)subscriberId,
            };

            if (otherRepository.AddToFriends(friends))
            {
                TempData["message-complete"] = string.Format("you added {0} to friends", subscriberId);
            }
            else
            {
                TempData["message-complete"] = string.Format("cant add {0} to friends", subscriberId);
            }

            return Redirect(returnUrl ?? Url.Action("Index", "FriendsList"));

        }

        public ActionResult delToFriends(Guid? ProfileId, Guid? subscriberId, string returnUrl, string login = null, string subscriberlogin = null)
        {
            if (ProfileId == null)
            {
                var Profile = GetProfileByLogin(login);
                ProfileId = Profile.ProfileId;
            }

            if (subscriberId == null)
            {
                var subscriber = GetProfileByLogin(login);
                subscriberId = subscriber.ProfileId;
            }

            var friends = new Friend()
            {
                ProfileId = (Guid)ProfileId,
                subscriberId = (Guid)subscriberId,
            };

            if (otherRepository.delToFriends(friends))
            {
                TempData["message-complete"] = string.Format("you del {0} to friends", subscriberId);
            }
            else
            {
                TempData["message-complete"] = string.Format("cant del {0} to friends", subscriberId);
            }

            return Redirect(returnUrl ?? Url.Action("Index", "FriendsList"));

        }

        public ActionResult setToFriends(Guid? ProfileId, Guid? subscriberId, string returnUrl, string login = null, string subscriberlogin = null)
        {
            Profile Profile = default(Profile);

            if (ProfileId == null)
            {
                Profile = GetProfileByLogin(login);
                ProfileId = Profile.ProfileId;
            }

            Profile subscriber = default(Profile);
            

            if (subscriberId == null)
            {
                subscriber = GetProfileByLogin(login);
                subscriberId = subscriber.ProfileId;
            }

            var friends = new Friend()
            {
                ProfileId = (Guid)ProfileId,
                subscriberId = (Guid)subscriberId,
            };

            if (otherRepository.setToFriends(friends))
            {
                TempData["message-complete"] = string.Format("{0} set {1} to friends", Profile.fName ?? "unknow", subscriber.fName ?? "unknow");
            }
            else
            {
                TempData["message-complete"] = string.Format("{0} cant set {1} to friends", Profile.fName ?? "unknow", subscriber.fName ?? "unknow");
            }

            return Redirect(returnUrl ?? Url.Action("Index", "FriendsList"));
        }

        public bool isFriend(Guid? ProfileId, Guid? subscriberId, string returnUrl, string login = null, string subscriberlogin = null)
        {
            if (ProfileId == null)
            {
                var Profile = GetProfileByLogin(login);
                ProfileId = Profile.ProfileId;
            }

            if (subscriberId == null)
            {
                var subscriber = GetProfileByLogin(login);
                subscriberId = subscriber.ProfileId;
            }

            var friends = new Friend()
            {
                ProfileId = (Guid)ProfileId,
                subscriberId = (Guid)subscriberId,
            };

            return otherRepository.isBestFriend(friends);
        }
    }
}