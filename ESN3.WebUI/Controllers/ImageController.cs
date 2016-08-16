using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESN3.WebUI.Controllers
{
    public class ImageController : Controller
    {
        private IOtherRepository otherRepository;

        public ImageController(IOtherRepository otherRepo)
        {
            otherRepository = otherRepo;
        }

        public FileContentResult GetImage(Guid? ImageId)
        {
            Image image = otherRepository.Images
                .FirstOrDefault(i => i.ImageId == ImageId);

            if (image != null)
            {
                return File(image.ImageData, image.ImageMimeType);
            }
            else
            {
                string filePath = @"C:\Users\A\Downloads\bp10\ESN3.WebUI\App_Data\camera_200.png";
                
                FileStream fs =  System.IO.File.OpenRead(filePath);
                
                BinaryReader br = new BinaryReader(fs);

                var img = br.ReadBytes((int)fs.Length);

                return File(img, "image/jpeg");
                
            }
        }

        public bool ExistImage(Guid ImageId)
        {
            return otherRepository.Images.FirstOrDefault(i => i.ImageId == ImageId) != default(Image);
        }


    }
}