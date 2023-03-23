using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sis_RosangelaCunha.Autenticacao;
using Sis_RosangelaCunha.DAO;
using Sis_RosangelaCunha.Models;
using Sis_RosangelaCunha.ViewModel;

namespace Sis_RosangelaCunha.Controllers
{
    //[Autorizar]
    public class PlanoController : BaseController
    {
#region Variaveis
        private PlanoDAO planoDAO = new PlanoDAO();
        #endregion
        public ActionResult Index()
        {
            return View();
        }
        #region Listar
        public ActionResult Listar(string busca)
        {
            //ViewBag.descricao = "descricaoPlano";

            var plano = new List<Plano>();

            if (string.IsNullOrEmpty(busca))
            {
                plano = planoDAO.BuscarTodos().ToList();
            }
            else
            {
                plano = planoDAO.BuscarPorDescricao(busca).ToList();
            }

            plano = plano.OrderBy(s => s.NomePlano).ToList();
            return PartialView("~/Views/Plano/_listar.cshtml", plano);
        }
        #endregion

        #region Novo
        public ActionResult Novo()
        {
            var plano = new PlanoViewModel();
            return PartialView("~/Views/Plano/_form.cshtml", plano);
        }
        [HttpPost]
        public ActionResult Novo(Plano plano)
        {
            var result = false;

            try
            {
                planoDAO.Gravar(plano);
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
            ViewBag.planoId = new MultiSelectList(planoDAO.BuscarTodos(), "planoId", "nomePlano");
            Plano plano = planoDAO.Buscar(id);
            return PartialView("~/Views/Plano/_form.cshtml", plano);
        }
        [HttpPost]
        public ActionResult Editar(Plano plano)
        {
            var result = false;

            try
            {
                planoDAO.Alterar(plano);
                result = true;

            }
            catch (Exception ex)
            {
                GravarLog("Plano", "Erro ao Gravar Usuario : " + ex.Message, 1);
                result = false;
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Excluir
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            var result = false;

            try
            {
                var m = planoDAO.Buscar(id);
                planoDAO.Excluir(m);
                result = true;
            }
            catch (Exception ex)
            {
                //TODO: LOG 
                GravarLog("Plano", "Erro ao Excluir Plano : " + ex.Message, 1);
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        //public ActionResult ListarPorDescricao(string descricaoPlano)
        //{
        //    return PartialView("ListarPorDescricao", planoDAO.BuscarPorDescricao(descricaoPlano));
        //}
    }
}