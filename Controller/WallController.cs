using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShareThoughts.Models;
using System.Collections.Generic;
using DbConnection;

namespace ShareThoughts.Controllers
{
    public class WallController : Controller
    {

        private readonly DbConnector _dbConnector;

        public WallController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("Board")]
        public IActionResult Board()
        {
            if (HttpContext.Session.GetInt32("userId") == null)
            {
                ViewBag.Error = "You need to login to do that!";
                return RedirectToAction("Index");
            }
            else
            {
                List<Dictionary<string, object>> result = _dbConnector.Query("SELECT concat(firstname,' ', lastname) as name, message, message_id, messages.created_at  FROM Users join messages on users_id = id order by messages.created_at desc;");
                ViewBag.Message = result;
                List<Dictionary<string, object>> comments = _dbConnector.Query("SELECT concat(firstname,' ', lastname) as name, comment, messages_id, comments.created_at  FROM Users join comments on users_id = id order by comments.created_at desc;");
                ViewBag.Comment = comments;
                // }
                return View("Wall");
            }

        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index", "Home");
        }

        [HttpPost]
        [Route("PostMessage")]
        public IActionResult PostMessage(String message)
        {
            if (HttpContext.Session.GetInt32("userId") == null)
            {
                ViewBag.Error = "You need to login to do that!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int? userid = HttpContext.Session.GetInt32("userId");
                _dbConnector.Query($"insert into messages (message, users_id) values ('{message}', {userid});");
                return RedirectToAction("Board");
                
            }
        }

        [HttpPost]
        [Route("PostComment")]
        public IActionResult PostComment(string comment, int msgid)
        {
            if (HttpContext.Session.GetInt32("userId") == null)
            {
                ViewBag.Error = "You need to login to do that!";
                return View("Index");
            }
            else
            {
                int? userid = HttpContext.Session.GetInt32("userId");
                if (ModelState.IsValid)
                {
                    _dbConnector.Query($"insert into comments (comment, users_id, messages_id) values ('{comment}', {userid}, {msgid})");

                    return RedirectToAction("Board");
                }
                return View("Index");
            }
        }
    }
}