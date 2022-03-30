using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingApp.Models;
using System.IO;

namespace OnlineShoppingApp.Controllers
{
    public class UploadController : Controller
    {
        DetailsDB DetailsDBObj = new DetailsDB();
        public ActionResult List()
        {
            return View(DetailsDBObj.ImageTables.ToList());
        }



        public ActionResult Create()
        {
            return View();
        }

        ImageTable ImageTableObj = new ImageTable();
        [HttpPost]
        public ActionResult Create(ImageTable ImageTableObj)
        {
            string fileName = Path.GetFileNameWithoutExtension(ImageTableObj.ImageFile.FileName);
            string extension = Path.GetExtension(ImageTableObj.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            ImageTableObj.ImagePath = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
            ImageTableObj.ImageFile.SaveAs(fileName);
            using (DetailsDB DetailsDBObj = new DetailsDB())
            {
                DetailsDBObj.ImageTables.Add(ImageTableObj);
                DetailsDBObj.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("List", "Upload");
        }



        public ActionResult Edit(int id)
        {
            ImageTable details = DetailsDBObj.ImageTables.Find(id);
            return View(details);
        }


        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit1(int id)
        {
            ImageTable details = DetailsDBObj.ImageTables.Find(id);
            UpdateModel(details);
            DetailsDBObj.SaveChanges();
            return RedirectToAction("List", "Upload");
        }



        public ActionResult Delete(int id)
        {
            ImageTable details = DetailsDBObj.ImageTables.Find(id);
            return View(details);

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete1(int id)
        {
            ImageTable details = DetailsDBObj.ImageTables.Find(id);
            DetailsDBObj.ImageTables.Remove(details);
            DetailsDBObj.SaveChanges();
            return RedirectToAction("List", "Upload");
        }
    }
}