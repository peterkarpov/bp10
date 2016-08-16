using ESN.Domain.Abstract;
using ESN3.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class MessagesController : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public MessagesController(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        public ActionResult Index(string login)
        {
            MessagesViewModel model = new MessagesViewModel();

            if (login != null)
                model.User = otherRepository.Users.FirstOrDefault(u => u.login == login);

            if (model.User != null)
                model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == model.User.UserId);

            var Messages = otherRepository.Messages.Where(m => m.from == model.Profile.ProfileId).GroupBy(m => m.to).ToList();

            model.Talks = new List<Talk1>();

            foreach (var item in Messages)
            {

                Talk1 Talk = new Talk1()
                {
                    Messages = item.Concat(otherRepository.Messages.Where(p=>p.from==item.Key)).OrderBy(m => m.creationTime).ToList(),
                    from = model.Profile,
                    to = repository.Profiles.FirstOrDefault(p => p.ProfileId == item.Key)
                };

                model.Talks.Add(Talk);

            }


            return View(model);
        }

        public ActionResult Talk(Talk1 Talk)
        {
            return View(Talk);
        }
    }
}