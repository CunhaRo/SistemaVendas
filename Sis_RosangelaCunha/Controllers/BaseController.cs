using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sis_RosangelaCunha.Controllers
{
    public class BaseController : Controller
    {

        public int IdUsuario { get; set; }


        public BaseController()
        {
            IdUsuario = 5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="local"></param>
        /// <param name="mensagem"></param>
        /// <param name="tipo"></param>
        public void GravarLog(string local, string mensagem, int tipo)
        {


        }
    }
}