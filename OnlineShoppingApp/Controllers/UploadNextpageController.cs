using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingApp.Models;
using System.IO;

namespace OnlineShoppingApp.Controllers
{
    public class UploadNextpageController : Controller
    {
        DetailsDB DetailsDBObj = new DetailsDB();
        public ActionResult ListNext()
        {
            return View(DetailsDBObj.ImageTableNexts.ToList());
        }


        [HttpGet]
        public ActionResult CreateNext()
        {
            return View();
        }

        ImageTableNext ImageTableNextObj = new ImageTableNext();
        [HttpPost]
        public ActionResult CreateNext(ImageTableNext ImageTableNextObj)
        {
            string fileName = Path.GetFileNameWithoutExtension(ImageTableNextObj.ImageFile.FileName);
            string extension = Path.GetExtension(ImageTableNextObj.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            ImageTableNextObj.ImagePath = "~/ImagesNext/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/ImagesNext/"), fileName);
            ImageTableNextObj.ImageFile.SaveAs(fileName);
            using (DetailsDB DetailsDBObj = new DetailsDB())
            {
                DetailsDBObj.ImageTableNexts.Add(ImageTableNextObj);
                DetailsDBObj.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("ListNext", "UploadNextpage");
        }



        public ActionResult EditNext(int id)
        {
            ImageTableNext details = DetailsDBObj.ImageTableNexts.Find(id);
            return View(details);
        }

        [HttpPost]
        [ActionName("EditNext")]
        public ActionResult EditNext1(int id)
        {
            ImageTableNext details = DetailsDBObj.ImageTableNexts.Find(id);
            UpdateModel(details);
            DetailsDBObj.SaveChanges();
            return RedirectToAction("ListNext", "UploadNextpage");
        }



        public ActionResult DeleteNext(int id)
        {
            ImageTableNext details = DetailsDBObj.ImageTableNexts.Find(id);
            return View(details);

        }

        [HttpPost]
        [ActionName("DeleteNext")]
        public ActionResult DeleteNext1(int id)
        {
            ImageTableNext details = DetailsDBObj.ImageTableNexts.Find(id);
            DetailsDBObj.ImageTableNexts.Remove(details);
            DetailsDBObj.SaveChanges();
            return RedirectToAction("ListNext", "UploadNextpage");
        }
    }
}