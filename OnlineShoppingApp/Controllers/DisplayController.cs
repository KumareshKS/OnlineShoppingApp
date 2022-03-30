using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingApp.Models;


namespace OnlineShoppingApp.Controllers
{
    public class DisplayController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


        public ActionResult Index(string Searching)
        {
            return View(DetailsDBObj.ImageTableNexts.Where(x => x.Name.Contains(Searching) || Searching == null).ToList());
        }



        DetailsDB DetailsDBObj = new DetailsDB();
        ImageTable ImageTableObj = new ImageTable();
        public ActionResult DisplayIndex(string Searching)
        {
            return View(DetailsDBObj.ImageTables.Where(x => x.Name.Contains(Searching) || Searching == null).ToList());
        }


        ImageTableNext ImageTableNextObj = new ImageTableNext();
        public ActionResult DisplayNext(int Id, string Searching)
        {
            ImageTableNext GetId = DetailsDBObj.ImageTableNexts.Where(x => x.ImageId == Id).FirstOrDefault();
            ViewBag.ImageId = GetId.ImageId;
            return View(DetailsDBObj.ImageTableNexts.Where(x => x.Name.Contains(Searching) || Searching == null).ToList());
        }

        
        public ActionResult NextPage(int Id, string ImagePath)
        {
            string name = (string)Session["Username"];
            if (DetailsDBObj.CartTables.Any(x => x.ImagePath == ImagePath && x.Username==name))
            {
                ViewBag.Msg = "You are already added to this product in your cart";
            }
            ImageTableNext Get = DetailsDBObj.ImageTableNexts.Single(x => x.Id == Id);
            return View(Get);
        }
    
    }
}