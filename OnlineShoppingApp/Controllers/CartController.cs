using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Controllers
{
    public class CartController : Controller
    {
        DetailsDB DetailsDBObj = new DetailsDB();
        CartTable CartTableObj = new CartTable();        
               
        public ActionResult AddToCart(ImageTableNext ImageTableNextObj)
        {
            using (DetailsDB DetailsDBObj = new DetailsDB())
            {
                var CartObj = new CartTable()
                {
                    UserId=(int)Session["UserId"],
                    Username=(string)Session["Username"],
                    ImagePath=(string)Session["ImagePath"],
                    Price=(int)Session["Price"],
                    Description=(string)Session["Description"],
                    Color = (string)Session["Color"],
                    Name = (string)Session["Name"],
                    Material_Type = (string)Session["Material_Type"],
                    Manufacturer= (string)Session["Manufacturer"],
                    Country_of_Origin= (string)Session["Country_of_Origin"],
                    Item_Weight = (string)Session["Item_Weight"],
                    Item_Dimensions_LxWxH = (string)Session["Item_Dimensions_LxWxH"],
                    Sport= (string)Session["Sport"],
                    Brand = (string)Session["Brand"],
                    Quantity=1
                };
                DetailsDBObj.CartTables.Add(CartObj);
                DetailsDBObj.SaveChanges();
            }
            return RedirectToAction("CartDetails", "Cart");
        }

        public ActionResult CartDetails()
        {
            return View(DetailsDBObj.CartTables.ToList());
        }


        public ActionResult DeleteCartDetails(int Id)
        {
            CartTable details = DetailsDBObj.CartTables.Find(Id);
            return View(details);
        }
        [HttpPost]
        [ActionName("DeleteCartDetails")]
        public ActionResult DeleteCartDetails1(int Id)
        {
            CartTable details = DetailsDBObj.CartTables.Find(Id);
            DetailsDBObj.CartTables.Remove(details);
            DetailsDBObj.SaveChanges();
            return RedirectToAction("CartDetails", "Cart");
        }
        
        public ActionResult Plus(int Id)
        {
            CartTable Row = DetailsDBObj.CartTables.Find(Id);
            Row.Quantity=++Row.Quantity;
            UpdateModel(Row);
            DetailsDBObj.SaveChanges();
            return RedirectToAction("CartDetails", "Cart");
        }
        public ActionResult Minus(int Id)
        {
            CartTable Row = DetailsDBObj.CartTables.Find(Id);
            Row.Quantity = --Row.Quantity;
            if (Row.Quantity > 0)
            {
                UpdateModel(Row);
                DetailsDBObj.SaveChanges();
            }           
            return RedirectToAction("CartDetails", "Cart");
        }
    }
}