using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Controllers
{
    public class ShippingController : Controller
    {
        DetailsDB DetailsDBObj = new DetailsDB();
        ShippingDetail ShippingDetailObj = new ShippingDetail();

        public ActionResult Shippingdetails(int num, string Message)
        {
            Session["Message"] = Message;
            
                if (DetailsDBObj.ShippingDetails.Any(x => x.UserId == num))
                {
                    ShippingDetail TableDetails = DetailsDBObj.ShippingDetails.Single(x=>x.UserId== num);
                    return View(TableDetails);
                }
                else
                {
                    return View();

                }
        }
        [HttpPost]
        public ActionResult Shippingdetails(ShippingDetail ShippingDetailObj )
        {
            int num = (int)Session["UserId"];

            if (DetailsDBObj.ShippingDetails.Any(x => x.UserId == num))
            {
                ShippingDetail TableDetails = DetailsDBObj.ShippingDetails.Single(x => x.UserId == num);
                UpdateModel(TableDetails);
                DetailsDBObj.SaveChanges();
            }
            else
            {
                var DetailsObj = new ShippingDetail()
                {
                    UserId = (int)Session["UserId"],
                    Username = (string)Session["Username"],
                    Firstname = ShippingDetailObj.Firstname,
                    Lastname = ShippingDetailObj.Lastname,
                    Address = ShippingDetailObj.Address,
                    City = ShippingDetailObj.City,
                    State = ShippingDetailObj.State,
                    Country = ShippingDetailObj.Country,
                    Pincode = ShippingDetailObj.Pincode,
                    Email = ShippingDetailObj.Email
                };
                DetailsDBObj.ShippingDetails.Add(DetailsObj);
                DetailsDBObj.SaveChanges();
                ModelState.Clear();

            }
            return RedirectToAction("Payment", "Shipping");
            

        }
        public ActionResult Payment()
        {
            if(Session["Message"] == null)
            {
                DetailsDB DetailsDBObj = new DetailsDB();
                foreach (var item in DetailsDBObj.CartTables.ToList())
                {
                    if (item.Username == (string)Session["Username"])
                    {
                        var OrderObj = new OrdersTable()
                        {
                            UserId = (int)Session["UserId"],
                            Username = (string)Session["Username"],
                            Name = item.Name,
                            Price = item.Price,
                            Brand = item.Brand,
                            Color = item.Color,
                            Quantity = item.Quantity,
                            ImagePath = item.ImagePath,
                        };
                        DetailsDBObj.OrdersTables.Add(OrderObj);
                        DetailsDBObj.SaveChanges();
                    }
                }
                return View();
            }
            else
            {
                var OrderObj = new OrdersTable()
                {
                    UserId = (int)Session["UserId"],
                    Username = (string)Session["Username"],
                    Name = (string)Session["Name"],
                    Price = (int)Session["Price"],
                    Brand = (string)Session["Brand"],
                    Color = (string)Session["Color"],
                    Quantity = 1,
                    ImagePath = (string)Session["ImagePath"],
                };
                DetailsDBObj.OrdersTables.Add(OrderObj);
                DetailsDBObj.SaveChanges();
                return View();
            }
            
        }
        public ActionResult Orders()
        {
            return View(DetailsDBObj.OrdersTables.ToList());
        }
    }
}