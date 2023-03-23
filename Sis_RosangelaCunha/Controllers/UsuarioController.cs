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
    //[Autorizar]
    public class UsuarioController : BaseController
    {
        #region Variaveis
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region Listar
        /// <summary>
        /// 
        /// </summary>
        /// <param name="busca"></param>
        /// <returns></returns>
        public ActionResult Listar(string busca)
        {
            ViewBag.nome = "nome";

            var usuario = new List<Usuario>();

            if (string.IsNullOrEmpty(busca))
            {
                usuario = usuarioDAO.BuscarTodos().ToList();
            }
            else
            {
                usuario = usuarioDAO.BuscarNome(busca).ToList();
            }

            usuario = usuario.OrderBy(s => s.Nome).ToList();
            return PartialView("~/Views/Usuario/_listar.cshtml", usuario);
        }
        #endregion
        #region Novo
        public ActionResult Novo()
        {
            var usuario = new Usuario();
            return PartialView("~/Views/Usuario/_form.cshtml", usuario);
        }
        [HttpPost]
        public ActionResult Novo(Usuario usuario)
        {
            var result = false;

            try
            {
                usuarioDAO.Gravar(usuario);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Editar
        public ActionResult Editar(int id)
        {
            ViewBag.usuarioId = new MultiSelectList(usuarioDAO.BuscarTodos(), "usuarioId", "nome");
            Usuario usuario = usuarioDAO.Buscar(id);
            return PartialView("~/Views/Usuario/_form.cshtml", usuario);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            var result = false;

            try
            {
                usuarioDAO.Editar(usuario);
                result = true;

            }
            catch (Exception ex)
            {
                GravarLog("Usuario", "Erro ao Gravar Usuario : " + ex.Message, 1);
                result = false;
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region Excluir
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            var result = false;
            
            try
            {
                var m = usuarioDAO.Buscar(id);
                usuarioDAO.Excluir(m);
                result = true;
            }
            catch (Exception ex)
            {
                //TODO: LOG 
                GravarLog("Usuario", "Erro ao Excluir Usuario : " + ex.Message, 1);
            }

            return Json(new {Resultado = result}, JsonRequestBehavior.AllowGet);

        }

        #endregion
        //public ActionResult AlterarSenha()
        //{
        //    return View();
        //}
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult AlterarSenha(Usuario usuario)
        //{
        //    try
        //    {
        //        Usuario user = Session["User"] as Usuario;

        //        if (user.Senha == usuario.SenhaAntiga)
        //        {
        //            user.Senha = usuario.Senha;
        //            usBO.AlterarSenha(user);
        //            return RedirectToAction("Sair", "Conta");
        //        }
        //        ModelState.AddModelError(string.Empty, "Senha atual inválida");
        //        return View();
        //    }
        //    catch (Exception erro)
        //    {
        //        ModelState.AddModelError(string.Empty, erro);
        //    }
        //    return View();

        //}
    }
}