using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class NewsController : Controller
    {
        [Inject]
        private IProfileRepository repository { get; set; }
        [Inject]
        private IOtherRepository otherRepository { get; set; }
        public NewsController()
        {
            repository = DependencyResolver.Current.GetService<IProfileRepository>();
            otherRepository = DependencyResolver.Current.GetService<IOtherRepository>();

        }

        public ActionResult Index()
        {
            ViewBag.NavSec = "News";
            ViewBag.NavNewsSec = "Index";

            return View();
        }

        public ActionResult AllNews()
        {
            var model = new List<News>();

            model = otherRepository.News.OrderByDescending(n => n.creationTime).ToList();

            return View(model);
        }

        [Authorize]
        public ActionResult OnFriendsNews(Guid? ProfileId, string login = null)
        {
            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }
            
            var news = from f in otherRepository.Friends
                       where f.ProfileId == ProfileId
                       join n in otherRepository.News on f.subscriberId equals n.ProfileId
                       select n;

            var model = news.ToList();

            return View(model);
        }

        [Authorize]
        public ActionResult OnUserNews(Guid? ProfileId, string login = null)
        {
            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }

            var model = from n in otherRepository.News
                        where n.ProfileId == ProfileId
                        select n;

            return View(model.ToList());
        }

        public ActionResult ShowOneNews(Guid NewsId)
        {
            var News = otherRepository.News.FirstOrDefault(n => n.NewsId == NewsId);

            return View(News);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateNews(Guid? ProfileId, string login = null)
        {

            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }


            var News = new News() { ProfileId = (Guid)ProfileId };

            ViewBag.AutorName = repository.Profiles.FirstOrDefault(p => p.ProfileId == News.ProfileId).fName;


            return View(News);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateNews(News News, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                News.NewsId = Guid.NewGuid();
                News.creationTime = DateTime.Now;

                if (otherRepository.SaveNews(News))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "News"));
                }
                else
                {
                    ModelState.AddModelError("", "Cant Save News");
                }
            }

            ViewBag.AutorName = repository.Profiles.FirstOrDefault(p => p.ProfileId == News.ProfileId).fName;


            return View();
        }

        [Authorize]
        public ActionResult DeleteNews(Guid NewsId, string returnUrl)
        {
            var news = otherRepository.News.FirstOrDefault(n => n.NewsId == NewsId);

            if (User.Identity.Name.Split('|')[1] != news.ProfileId.ToString())
            {
                TempData["message-error"] = string.Format("News Deleted Failure");
            }

            if (otherRepository.DeleteNews(NewsId))
            {
                TempData["message-complete"] = string.Format("News Deleted");
                //return Redirect(returnUrl ?? Url.Action("Index", "News"));
            }
            else
            {
                TempData["message-error"] = string.Format("News Deleted Failure");
            }

            return View("Index");
        }

        
    }
}