using System.Linq;
using System.Web.Mvc;
using ESN.Domain.Entities;
using ESN.Domain.Abstract;
using ESN.WebUI.Models;
using System;

namespace ESN.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProfileRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProfileRepository repo, IOrderProcessor processor)
        {
            repository = repo;
            orderProcessor = processor;

        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, Guid ProfileId, string returnUrl)
        {
            Profile profile = repository.Profiles
                .FirstOrDefault(g => g.ProfileId == ProfileId);

            if (profile != null)
            {
                cart.AddItem(profile, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, Guid ProfileId, string returnUrl)
        {
            Profile profile = repository.Profiles
                .FirstOrDefault(g => g.ProfileId == ProfileId);

            if (profile != null)
            {
                cart.RemoveLine(profile);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //public Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}