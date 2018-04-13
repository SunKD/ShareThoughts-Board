using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShareThoughts.Models;
using System.Collections.Generic;
using DbConnection;

namespace ShareThoughts.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly DbConnector _dbConnector;

        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _dbConnector.Query($"insert into users (firstname, lastname, email, password) values ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Password}');");
                List<Dictionary<string, object>> currentUser = _dbConnector.Query($"SELECT * FROM USERS WHERE EMAIL = '{user.Email}'");
                HttpContext.Session.SetInt32("userId", (int)currentUser[0]["id"]);
                return RedirectToAction("Board", "Wall");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string loginemail, string loginpw){
            List<Dictionary<string, object>> user = _dbConnector.Query($"SELECT * FROM USERS WHERE EMAIL = '{loginemail}'");
            if(user.Count > 0){
                if((string)user[0]["password"] == loginpw){
                    HttpContext.Session.SetInt32("userId", (int)user[0]["id"]);
                    TempData["user"]= user[0]["firstname"];
                    return RedirectToAction("Board", "Wall");
                }
                else{
                    ViewBag.errors = "Password is not matching!";
                    ViewBag.fail = 1;
                    return View("Index");
                }
            }
            ViewBag.errors = "Email is not exsist!";
            ViewBag.fail = 1;
            return View("Index");
        }
    }
}