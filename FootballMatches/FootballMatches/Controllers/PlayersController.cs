﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballMatches.Controllers
{
    public class PlayersController : Controller
    {
        // GET: Players
        public ActionResult Index(int id)
        {
            ViewBag.ParentId = id;
            return View();
        }
    }
}