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
    public class LikeController : Controller
    {
        [Inject]
        private IProfileRepository repository { get; set; }
        [Inject]
        private IOtherRepository otherRepository { get; set; }
        public LikeController()
        {
            repository = DependencyResolver.Current.GetService<IProfileRepository>();
            otherRepository = DependencyResolver.Current.GetService<IOtherRepository>();

        }

        [Authorize]
        public ActionResult SetLike(Guid TargetId, Guid? ProfileId, string login, string returnUrl)
        {
            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }

            Like Like = new Like();
            Like.ProfileId = (Guid)ProfileId;
            Like.TargetId = TargetId;

            otherRepository.SetLike(Like, "News");

            return Redirect(returnUrl ?? Url.Action("Index", "News"));
        }

        [Authorize]
        public ActionResult SetDislike(Guid TargetId, Guid? ProfileId, string login, string returnUrl)
        {
            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                {
                    ProfileId = User.UserId;
                }
            }

            Dislike Dislike = new Dislike();
            Dislike.DislikeId = Guid.NewGuid();
            Dislike.ProfileId = (Guid)ProfileId;
            Dislike.TargetId = TargetId;

            otherRepository.SetDislike(Dislike);

            return Redirect(returnUrl ?? Url.Action("Index", "News"));

        }

        public int GetDislike(Guid? TargetId)
        {
            return otherRepository.Dislikes.Where(t => t.TargetId == TargetId).Count();
        }
    }
}