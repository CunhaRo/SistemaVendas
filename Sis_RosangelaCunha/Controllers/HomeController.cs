using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sis_RosangelaCunha.Autenticacao;
using Sis_RosangelaCunha.DAO;
using Sis_RosangelaCunha.Models;

namespace Sis_RosangelaCunha.Controllers
{
    public class HomeController : Controller
    {
         //[Autorizar]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}