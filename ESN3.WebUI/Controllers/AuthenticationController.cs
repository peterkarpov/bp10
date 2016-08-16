using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN3.WebUI.Infrastructure.Abstract;
using ESN3.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class AuthenticationController : Controller
    {
        IAuthProvider authProvider;
        public AuthenticationController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
                model.ConfirmPassword = null;

                User user = new User
                {
                    email = model.Email,
                    login = model.UserName,
                    password = model.Password,
                    UserId = Guid.NewGuid(),
                };

                if (authProvider.Registrate(user))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Authentication"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(AuthenticationViewModel model, string returnUrl)
        {



            if (ModelState.IsValid)
            {
                model.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password)));

                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Authentication"));
                }
                else
                {
                    

                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult SignOut(string returnUrl)
        {
            authProvider.SignOut();
            
            return Redirect(returnUrl ?? Url.Action("Index", "Authentication"));
        }

        public ActionResult UserEdit(string login, string returnUrl)
        {
            RegistrationViewModel model = new RegistrationViewModel();

            ESN.Domain.Abstract.IOtherRepository otherContext = new ESN.Domain.Concrete.EFOtherRepository();

            User user = otherContext.Users.FirstOrDefault(u => u.login == login);

            model.UserName = user.login;
            model.Email = user.email;

            return View(model);
        }

        [HttpPost]
        public ActionResult UserEdit(RegistrationViewModel model, string returnUrl, IOtherRepository repo)
        {
            IOtherRepository otherRepository = repo;

            if (ModelState.IsValid)
            {
                model.Password = model.Password.GetHashCode().ToString();
                model.ConfirmPassword = null;

                User user = new User
                {
                    email = model.Email,
                    login = model.UserName,
                    password = model.Password,
                };

                if (otherRepository.EditUser(user))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Authentication"));
                }
                else
                {
                    ModelState.AddModelError("", "wrong data");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public PartialViewResult Ajax()
        {
            return PartialView(new AuthenticationViewModel());
        }

        [HttpPost]
        public ActionResult Ajax(AuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    //return View("_Reload");
                    return RedirectToAction("Index", "Authentication");
                }
                ModelState["Password"].Errors.Add("wrong password or login");
            }
            return View(model);
        }
    }
}