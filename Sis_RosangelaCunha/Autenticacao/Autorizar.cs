using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sis_RosangelaCunha.Autenticacao
{
    public class Autorizar: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var Autorizado = base.AuthorizeCore(httpContext);
            if (!Autorizado)
            {
                return false;
            }
            var usuario = httpContext.Session["User"];
            if (usuario == null)
            {
                return false;
            }
            return true;
        }
    }
}