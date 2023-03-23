using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sis_RosangelaCunha.DAO;
using Sis_RosangelaCunha.Models;
using System.Web.Security;

namespace Sis_RosangelaCunha.Controllers
{
    public class ContaController : Controller
    {
         
        //public ActionResult Login()
        //{
        //    return PartialView();
        //}
        //[HttpPost]
        //public ActionResult Login(string login, string senha)
        //{
        //    try
        //    {
        //        Usuario usuario = new Usuario();
        //        usuario.Login = login;
        //        usuario.Senha = senha;

        //        Usuario user = usuarioBO.BuscarUsuario(usuario);
        //        if (user !=null)
        //        {
        //            Session["User"] = user;
        //            FormsAuthentication.SetAuthCookie(user.Login, false);
        //            if(user.Senha == senha)
        //            {
        //                return RedirectToAction("AlterarSenha", "Usuario");
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError(string.Empty, "Acesso Inválido");
        //    }
        //    ModelState.AddModelError(string.Empty, "Acesso Inválido");
        //    return PartialView();
        //}
        //public ActionResult Sair()
        //{
        //    Session["User"] = null;
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Login", "Conta");
        //}
    }
}