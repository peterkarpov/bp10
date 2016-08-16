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
    public class Messages4Controller : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public Messages4Controller(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        // GET: Messages3
        public ActionResult Index(Guid? ProfileId)
        {

            if ((ProfileId == null) & (User.Identity.IsAuthenticated))
                ProfileId = Guid.Parse(User.Identity.Name.Split('|')[1]);

            var Profile = repository.Profiles.FirstOrDefault(u => u.ProfileId == ProfileId);

            if (Profile == null)
            {
                TempData["message-error"] = string.Format("profile not exist");
                ModelState.AddModelError("", "profile not exist");
            }

            if (!User.Identity.IsAuthenticated)
            {
                TempData["message-error"] = string.Format("you not authenticated");
                ModelState.AddModelError("", "you not authenticated");
            }

            return View(Profile);
        }

        public ActionResult Talks(Guid? ProfileId, string login)
        {
            if (ProfileId == null)
                ProfileId = otherRepository.Users.FirstOrDefault(u => u.login == login).UserId;

            Messages4ViewModel model = new Messages4ViewModel();

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            model.Talks = otherRepository.Talks.Where(t => (t.Profile1Id == model.Profile.ProfileId) | (t.Profile2Id == model.Profile.ProfileId)).OrderBy(o => o.Messages.First().creationTime).ToList();

            return View(model);
        }

        [Authorize]
        public ActionResult OneTalk(Guid? TalkId = null, int page = 1, int pageSize = 4)
        {
            TalkViewModel model = new TalkViewModel();

            Talk Talk = otherRepository.Talks.FirstOrDefault(t => t.TalkId == TalkId);

            model.Messages = new List<Message>();
            model.Messages = Talk.Messages.OrderByDescending(m=>m.creationTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Guid fromProfileId = Guid.Parse(User.Identity.Name.Split('|')[1]);

            model.from = repository.Profiles.FirstOrDefault(p => p.ProfileId == fromProfileId);

            Guid toProfileId = fromProfileId == Talk.Profile1Id ? Talk.Profile2Id : Talk.Profile1Id;

            model.to = repository.Profiles.FirstOrDefault(p => p.ProfileId == toProfileId);

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = Talk.Messages.Count(),
            };

            model.pageSize = pageSize;

            model.TalkId = TalkId;

            return View(model);

        }

        [Authorize]
        [HttpGet]
        public ActionResult SendMessage(Guid fromProfileId, Guid toProfileId)
        {
            var message = new Message();

            message.from = (Guid)fromProfileId;
            message.to = (Guid)toProfileId;

            return View(message);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult SendMessagePartial(Guid fromProfileId, Guid toProfileId)
        {
            var message = new Message();

            message.from = (Guid)fromProfileId;
            message.to = (Guid)toProfileId;

            message.ProfileFrom = repository.Profiles.FirstOrDefault(p => p.ProfileId == fromProfileId);

            return PartialView(message);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            message.MessageId = Guid.NewGuid();
            message.creationTime = DateTime.Now;

            //Talk Talk = otherRepository.Talks.FirstOrDefault(t =>
            //(((t.Profile1Id == message.from) & (t.Profile1Id == message.to)) |
            //((t.Profile2Id == message.from) & (t.Profile2Id == message.to))
            //));

            var Talk1 = from t in otherRepository.Talks
                       where t.Profile1Id == message.@from
                       where t.Profile2Id == message.to
                       select t;

            var Talk2 = from t in otherRepository.Talks
                       where t.Profile2Id == message.@from
                       where t.Profile1Id == message.to
                       select t;

            var Talk = Talk1.First() ?? Talk2.First();

            

            if (Talk == null)
            {
                Talk = new Talk
                {
                    Profile1Id = message.from,
                    Profile2Id = message.to,
                    TalkId = Guid.NewGuid(),
                };
                otherRepository.SaveTalk(Talk);
            }

            message.TalkId = Talk.TalkId;
            otherRepository.SaveMessage(message);

            return RedirectToAction("OneTalk", new { TalkId = message.TalkId });
        }


    }
}