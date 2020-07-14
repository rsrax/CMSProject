﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CMSProject.Models.ViewModel;
using CMSProject.Models.EntityManager;
using CMSProject.Security;

namespace CMSProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}