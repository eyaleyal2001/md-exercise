﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MassdropExerciseData;

namespace MassdropExercise.Controllers
{
    public class HomeController : Controller
    {    

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
