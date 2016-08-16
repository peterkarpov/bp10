using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using ESN3.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ESN3.WebUI.Controllers
{
    public class Photobook2Controller : Controller
    {
        private IProfileRepository repository;
        private IOtherRepository otherRepository;

        public Photobook2Controller(IProfileRepository repo, IOtherRepository otherRepo)
        {
            repository = repo;
            otherRepository = otherRepo;
        }

        private string NavSec = "Photobook2";
        private int PageSize = 4;


        public ActionResult Index()
        {
            //>
            
            //<

            ViewBag.NavSec = NavSec;
            ViewBag.NavPhotobookSec = "Index";
            return View();
        }

        public ActionResult All(int page = 1)
        {
            Photobook2ViewModel model = new Photobook2ViewModel();

            model.Photobooks = otherRepository.Photobooks
                .OrderBy(p => p.Title)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = otherRepository.Photobooks.Count(),
            };

            ViewBag.TotalItems = model.PagingInfo.TotalItems;
            ViewBag.NavSec = NavSec;
            ViewBag.NavPhotobookSec = "All";

            //return View("List", model);
            return View(model); //list
        }

        public ActionResult OnUser(string UserIdString, string login, int page = 1)
        {
            Photobook2ViewModel model = new Photobook2ViewModel();

            Guid UserId = default(Guid);

            if (!Guid.TryParse(UserIdString, out UserId))
            {
                User user = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (user != null) UserId = user.UserId;
            }

            model.Photobooks = otherRepository.Photobooks.Where(p => p.ProfileId == UserId)
                .OrderBy(p => p.Title)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = otherRepository.Photobooks.Where(p => p.ProfileId == UserId).Count(),
            };

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == UserId);

            if (model.Profile == null)
            {
                TempData["message-error"] = string.Format("Profile with login \"{0}\" not exist", login);
                return RedirectToAction("Index");
            }

            ViewBag.NavSec = NavSec;
            ViewBag.NavPhotobookSec = "OnUser";

            return View(model); //list
        }

        public ActionResult OnFriends(string UserIdString, string login, int page = 1)
        {
            Photobook2ViewModel model = new Photobook2ViewModel();

            //var Friends = otherRepository.Friends.Where(f => f.ProfileId.ToString() == UserId).Select(p=>p.FriendId.ToString()).ToList();

            //model.Photobooks = new List<Photobook>();

            //foreach (var friend in Friends)
            //{
            //    model.Photobooks.Concat(otherRepository.Photobooks.Where(p => p.ProfileId.ToString() == friend).ToList());
            //}

            //var photobooksOnFriends = from u in otherRepository.Users
            //                          from p in repository.Profiles
            //                          from f in otherRepository.Friends
            //                          from ph in otherRepository.Photobooks
            //                          where u.login == "login1"
            //                          where p.ProfileId == u.UserId
            //                          where f.ProfileId == p.ProfileId
            //                          where ph.ProfileId == f.subscriberId
            //                          select ph;

            Guid UserId = default(Guid);

            if (!Guid.TryParse(UserIdString, out UserId))
            {
                User user = otherRepository.Users.FirstOrDefault(u => u.login == login);
                if (user != null) UserId = user.UserId;
            }

            //var photobooksOnFriends = from f in otherRepository.Friends
            //                          from ph in otherRepository.Photobooks
            //                          where f.ProfileId == UserId
            //                          where ph.ProfileId == f.subscriberId
            //                          select ph;

            var photobooksOnFriends = from f in otherRepository.Friends
                                      where f.ProfileId == UserId
                                      join ph in otherRepository.Photobooks on f.subscriberId equals ph.ProfileId
                                      select ph;

            //var result = (from member in memberList
            //              join document in Archive on member.Id equals document.MemberId
            //              select new ArchiveRecord { member = member, documentId = document.Id }).ToList();

            model.Photobooks = photobooksOnFriends
                .OrderBy(p => p.Title)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = photobooksOnFriends.Count(),
            };

            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == UserId);

            if (model.Profile == null)
            {
                TempData["message-error"] = string.Format("Profile with login \"{0}\" not exist", login);
                return RedirectToAction("Index");
            }

            ViewBag.NavSec = NavSec;
            ViewBag.NavPhotobookSec = "OnFriends";

            return View(model); //list
        }

        public ActionResult OnePhotobook(Guid PhotobookId, int page = 1, string searchByFileName = null)
        {
            Photobook2ViewModel model = new Photobook2ViewModel();

            IEnumerable<Image> imagesOnPhotobook;

            if (searchByFileName == null)
            {
                imagesOnPhotobook = from i in otherRepository.Images
                                    where i.PhotobookId == PhotobookId
                                    select i;
            }
            else
            {
                imagesOnPhotobook = from i in otherRepository.Images
                                    where i.PhotobookId == PhotobookId
                                    where i.fileName.ToUpper().Contains(searchByFileName.ToUpper()) 
                                    select i;
            }


            model.Images = imagesOnPhotobook
                .OrderBy(p => p.ImageId)
                .Skip((page - 1) * 6)   //PageSize
                .Take(6)    //PageSize
                .OrderBy(p => p.DateOfLoad)
                .ToList();



            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 4,
                TotalItems = imagesOnPhotobook.Count(),
            };

            ViewBag.TotalItems = model.PagingInfo.TotalItems;

            model.Photobooks = new List<Photobook> { otherRepository.Photobooks.FirstOrDefault(p => p.PhotobookId == PhotobookId) };
            model.Profile = repository.Profiles.FirstOrDefault(p => p.ProfileId == model.Photobooks.First().ProfileId);

            return View(model); //one
        }

        public ActionResult AddImageOnPhotobook(Guid PhotobookId)
        {
            var newImage = new Image();
            newImage.ImageId = Guid.NewGuid();
            newImage.PhotobookId = PhotobookId;

            return View(newImage);
        }

        [HttpPost]
        public ActionResult AddImageOnPhotobook(Image Image, HttpPostedFileBase uploadImage = null)
        {
            if (uploadImage.ContentType != null)
            {
                if (uploadImage != null)
                {
                    Image.ImageMimeType = uploadImage.ContentType;
                    Image.ImageData = new byte[uploadImage.ContentLength];
                    uploadImage.InputStream.Read(Image.ImageData, 0, uploadImage.ContentLength);
                }
                otherRepository.SaveImage(Image);
                TempData["message-complete"] = string.Format("update complete");
                return RedirectToAction("OnePhotobook", new { PhotobookId = Image.PhotobookId });
            }
            else
            {
                // Что-то не так со значениями данных
                return View(Image);
            }
        }

        public ActionResult AddImageOnPhotobookV2(Guid PhotobookId)
        {
            var newImage = new Image();
            newImage.ImageId = Guid.NewGuid();
            newImage.PhotobookId = PhotobookId;

            return View(newImage);
        }

        [HttpPost]
        public ActionResult AddImageOnPhotobookV2(Image Image, HttpPostedFileBase uploadImage)
        {


            //
            if (Request.Files.Count > 1)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = this.Request.Files[i] as HttpPostedFileBase;

                    byte[] imgbyte = new byte[file.ContentLength];
                    file.InputStream.Read(imgbyte, 0, imgbyte.Length);

                    var MIME = file.ContentType;
                    var imgb = imgbyte;
                    var fileName = file.FileName;

                    var img = new Image() { ImageData = imgbyte, ImageId = Guid.NewGuid(), ImageMimeType = MIME, PhotobookId = Image.PhotobookId, fileName = fileName };

                    otherRepository.SaveImage(img);
                }
            }
            else
            {
                //

                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }

                Image.ImageMimeType = uploadImage.ContentType;
                Image.ImageData = imageData;

                otherRepository.SaveImage(Image);
            }
            TempData["message-complete"] = string.Format("update complete");
            return RedirectToAction("OnePhotobook", new { PhotobookId = Image.PhotobookId });
        }

        [HttpGet]
        public ActionResult AddPhotobook(Guid ProfileId)
        {
            ViewBag.NavPhotobookSec = "AddPhotobook";

            return View(new Photobook() { ProfileId = ProfileId });
        }

        [HttpPost]
        public ActionResult AddPhotobook(Photobook Photobook)
        {
            if (ModelState.IsValid)
            {
                otherRepository.SavePhotobook(Photobook);
            }
            else
            {
                TempData["message-error"] = string.Format("update error");
                return RedirectToAction("Index");
            }

            var news = new News()
            {
                creationTime = DateTime.Now,
                NewsId = Guid.NewGuid(),
                ProfileId = Photobook.ProfileId,
                theme = String.Format("[Automatic message:] I'm create new photobook: " + Photobook.Title)
            };

            otherRepository.SaveNews(news);

            TempData["message-complete"] = string.Format("update complete");
            return RedirectToAction("OnePhotobook", new { PhotobookId = Photobook.PhotobookId });
        }
    }
}