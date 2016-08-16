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
    public class CommentsController : Controller
    {
        [Inject]
        private IProfileRepository repository { get; set; }
        [Inject]
        private IOtherRepository otherRepository { get; set; }
        public CommentsController()
        {
            repository = DependencyResolver.Current.GetService<IProfileRepository>();
            otherRepository = DependencyResolver.Current.GetService<IOtherRepository>();

        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CommentsOnTarget(Guid TargetId, int? commentsCount = null)
        {
            var model = new List<Comment>();

            var comments = otherRepository.Comments.Where(c => c.TargetId == TargetId).OrderByDescending(c => c.creationTime);

            if (commentsCount == null)
            {
                model = comments.ToList();
            }
            else
            {
                model = comments.Take((int)commentsCount).ToList();
            }



            return PartialView(model);
        }

        [Authorize]
        public PartialViewResult AddComment(Guid TargetId)
        {
            Comment newComment = new Comment();

            newComment.ProfileId = Guid.Parse(User.Identity.Name.Split('|')[1]);
            newComment.TargetId = TargetId;

            return PartialView(newComment);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(Comment newComment, string returnUrl)
        {
            newComment.CommentId = Guid.NewGuid();
            newComment.creationTime = DateTime.Now;

            if (otherRepository.SaveComment(newComment))
            {
                TempData["message-complete"] = string.Format("News Deleted");
                return Redirect(returnUrl ?? Url.Action("Index", "Comments"));
            }
            
            return View("Index");
        }

    }
}