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
    public class Messages3Controller : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public Messages3Controller(IProfileRepository repo, IOtherRepository otherRepo)
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

            Messages3ViewModel model = new Messages3ViewModel();

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == ProfileId);

            model.Talks = new List<Talk3>();

            //var companions = otherRepository.Messages.Where(m => m.from == model.Profile.ProfileId).Select(g => g.to).Distinct().ToList();

            //foreach (var companion in companions)
            //{
            //    var Talk = new Talk3();
            //    Talk.from = model.Profile;
            //    Talk.to = repository.Profiles.FirstOrDefault(t => t.ProfileId == companion);

            //    Talk.Messages = new List<ESN.Domain.Entities.Message>();

            //    var fromProfile = otherRepository.Messages.Where(f => f.from == Talk.from.ProfileId).Where(t => t.to == Talk.to.ProfileId);
            //    var toProfile = otherRepository.Messages.Where(f => f.from == Talk.to.ProfileId).Where(t => t.to == Talk.from.ProfileId);

            //    Talk.Messages = fromProfile.Concat(toProfile).Distinct().OrderByDescending(m => m.creationTime).ToList();

            //    model.Talks.Add(Talk);
            //}



            //
            var companionsTo = otherRepository.Messages.Where(m => m.to == model.Profile.ProfileId).Select(g => g.from).Distinct().ToList();
            var companionsFrom = otherRepository.Messages.Where(m => m.from == model.Profile.ProfileId).Select(g => g.to).Distinct().ToList();

            var companions = companionsTo.Concat(companionsFrom).Distinct();

            foreach (var companion in companions)
            {
                var Talk = new Talk3();
                Talk.from = model.Profile;
                Talk.to = repository.Profiles.FirstOrDefault(t => t.ProfileId == companion);

                Talk.Messages = new List<ESN.Domain.Entities.Message>();

                var fromProfile = otherRepository.Messages.Where(f => f.from == Talk.from.ProfileId).Where(t => t.to == Talk.to.ProfileId);
                var toProfile = otherRepository.Messages.Where(f => f.from == Talk.to.ProfileId).Where(t => t.to == Talk.from.ProfileId);

                Talk.Messages = fromProfile.Concat(toProfile).Distinct().OrderByDescending(m => m.creationTime).ToList();

                model.Talks.Add(Talk);
            }

            return View(model);


        }

    }
}