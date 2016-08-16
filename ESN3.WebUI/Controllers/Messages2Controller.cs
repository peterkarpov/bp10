using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Models
{
    public class Messages2Controller : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public Messages2Controller(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        private string NavSec = "Messages2";
        private int PageSize = 4;

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Talks(Guid? ProfileId, string login = null)
        {
            if (ProfileId == null)
            {
                var User = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (User != null)
                    ProfileId = User.UserId;
            }


            Messages2ViewModel model = new Messages2ViewModel();

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            if (model.Profile == null)
            {
                TempData["message-error"] = string.Format("error send message");
                return View("Index");
            }

            var messagesTo = otherRepository.Messages.Where(t => t.from == ProfileId);
            var messgesFrom = otherRepository.Messages.Where(f => f.to == ProfileId);

            var Messages = messagesTo.Concat(messgesFrom).Distinct().GroupBy(m => m.to);

            model.Talks = new List<Talk1>();

            foreach (var item in Messages)
            {

                Talk1 Talk = new Talk1()
                {
                    Messages = item.Concat(otherRepository.Messages.Where(p => p.from == item.Key)).Distinct().OrderBy(m => m.creationTime).ToList(),
                    from = model.Profile,
                    to = repository.Profiles.FirstOrDefault(p => p.ProfileId == item.Key)
                };

                model.Talks.Add(Talk);

            }

            return View(model);
        }

        [Authorize]
        public ActionResult SendMessage(string stringProfileId = null, string login = null, string stringToProfileId = null, string loginToProfile = null)
        {


            Guid ProfileId = default(Guid);
            if (!Guid.TryParse(stringProfileId, out ProfileId)) { };

            Guid toId = default(Guid);
            if (!Guid.TryParse(stringToProfileId, out toId)) { };

            if (login != null)
            {
                var Userto = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (Userto.login != User.Identity.Name)
                {
                    TempData["message-error"] = string.Format("error send message");
                    return View("Index");
                }

                if (Userto != null) ProfileId = Userto.UserId;
            }

            if (loginToProfile != null)
            {
                var Userfrom = otherRepository.Users.FirstOrDefault(u => u.login == loginToProfile);
                if (Userfrom != null) toId = Userfrom.UserId;
            }

            var newMessage = new Message();
            newMessage.to = toId;
            newMessage.from = ProfileId;


            return View(newMessage);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            message.creationTime = DateTime.Now;
            message.MessageId = Guid.NewGuid();

            if (otherRepository.SaveMessage(message))
            {
                TempData["message-complete"] = string.Format("message sended");
            }
            else
            {
                TempData["message-error"] = string.Format("error send message");
            }


            return View("SendMessage", message);

        }

        //public ActionResult OneTalk(string stringProfileId = null, string login = null, string stringToProfileId = null, string loginTo = null)
        //{
        //    Guid ProfileId = default(Guid);
        //    if (!Guid.TryParse(stringProfileId, out ProfileId)) { };

        //    Guid toId = default(Guid);
        //    if (!Guid.TryParse(stringToProfileId, out toId)) { };

        //    if (login != null)
        //    {
        //        var Userto = otherRepository.Users.FirstOrDefault(u => u.login == login);
        //        if (Userto.login != User.Identity.Name)
        //        {
        //            TempData["message-error"] = string.Format("error send message");
        //            return View("Index");
        //        }

        //        if (Userto != null) ProfileId = Userto.UserId;
        //    }

        //    if (loginTo != null)
        //    {
        //        var Userfrom = otherRepository.Users.FirstOrDefault(u => u.login == loginTo);
        //        if (Userfrom != null) toId = Userfrom.UserId;
        //    }

        //    var Talk = new Talk();

        //    //var toMessage = otherRepository.Messages.Where(m => m.to == toId);
        //    //var fromMessage = otherRepository.Messages.Where(s => s.from == ProfileId);

        //    var toMessage = from t in otherRepository.Messages
        //                    where t.@from == ProfileId
        //                    where t.to == toId
        //                    select t;

        //    var fromMessage = from t in otherRepository.Messages
        //                      where t.@from == toId
        //                      where t.to == ProfileId
        //                      select t;


        //    Talk.Messages = toMessage.Concat(fromMessage).OrderByDescending(o => o.creationTime).ToList();
        //    Talk.to = repository.Profiles.FirstOrDefault(p => p.ProfileId == toId);
        //    Talk.from = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

        //    return View(Talk);
        //}

        public ActionResult OneTalk(Guid? ProfileId, Guid? ToProfileId, string login = null, string toLogin = null)
        {


            Talk1 Talk = new Talk1();

            Talk.from = repository.Profiles.FirstOrDefault(f => f.ProfileId == ProfileId);
            Talk.to = repository.Profiles.FirstOrDefault(t => t.ProfileId == ToProfileId);

            var messagesTo = from m in otherRepository.Messages
                             where m.@from == ProfileId
                             where m.to == ToProfileId
                             select m;

            var messagesFrom = from m in otherRepository.Messages
                               where m.@from == ToProfileId
                               where m.to == ProfileId
                               select m;

            Talk.Messages = messagesTo.Concat(messagesFrom).Distinct().OrderByDescending(o => o.creationTime).ToList();

            return View(Talk);
        }
    }
}