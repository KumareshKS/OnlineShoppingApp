using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Controllers
{
    public class CustomerLoginController : Controller
    {
        public ActionResult Register()
        {
            RegisterTable RegisterTableObj = new RegisterTable();
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterTable RegisterTableObj)
        {
            using (UserDB UserDBObj = new UserDB())
            {
                if (UserDBObj.RegisterTables.Any(x => x.Username == RegisterTableObj.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exist.";
                    return View();
                }
                else if(RegisterTableObj.Password != RegisterTableObj.ConfirmPassword)
                {                   
                    return View();
                }
                UserDBObj.RegisterTables.Add(RegisterTableObj);
                UserDBObj.SaveChanges();
                ModelState.Clear();
            }
            return RedirectToAction("Login", "CustomerLogin");
        }


        public ActionResult Login()
        {
            LoginTable LoginTableObj = new LoginTable();
            return View();
        }
        RegisterTable RegisterTableObj = new RegisterTable();
        [HttpPost]
        public ActionResult Login(LoginTable LoginTableObj)
        {
            Session["UserId"] = 9999;
            ViewBag.Username = "kumaresh";
            ViewBag.Password = "2609";
            using (UserDB UserDBObj = new UserDB())
            {
                if (ViewBag.Username == LoginTableObj.Username && ViewBag.Password == LoginTableObj.Password)
                {
                    Session["Username"] = LoginTableObj.Username;
                    ViewBag.username = LoginTableObj.Username;
                    return RedirectToAction("List", "Upload");
                }
                else
                {
                    RegisterTable UserDetails = UserDBObj.RegisterTables.Where(x => x.Username == LoginTableObj.Username && x.Password == LoginTableObj.Password).FirstOrDefault();
                    ViewBag.username = LoginTableObj.Username;

                    if (UserDetails == null)
                    {
                        ViewBag.ErrorMessage = "Invalid Username Or Password";
                        return View();
                    }
                    else
                    { 
                        Session["UserId"] = UserDetails.UserID;
                        Session["Username"] = UserDetails.Username;
                        return RedirectToAction("Index", "Display");
                    }
                }
            }
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "CustomerLogin");
        }      
        
    }
}