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
    public class DescricaoPlanoController : BaseController
    {
        // GET: DescricaoPlano
        public ActionResult Index()
        {
            return View();
        }
        #region Variaveis
        private DescricaoPlanoDAO descplanoDAO = new DescricaoPlanoDAO();
        #endregion
        #region Listar
        public ActionResult Listar(string busca)
        {


            var descplano = new List<DescricaoPlano>();

            if (string.IsNullOrEmpty(busca))
            {
                descplano = descplanoDAO.BuscarTodos().ToList();
            }
            else
            {
                descplano = descplanoDAO.BuscarPorDescricao(busca).ToList();
            }

            descplano = descplano.OrderBy(s => s.Descricao).ToList();
            return PartialView("~/Views/DescricaoPlano/_listar.cshtml", descplano);
        }
        #endregion

        #region Novo
        public ActionResult Novo()
        {
            var descPlano = new DescricaoPlano();
            return PartialView("~/Views/DescricaoPlano/_form.cshtml", descPlano);
        }
        [HttpPost]
        public ActionResult Novo(DescricaoPlano descPlano)
        {
            var result = false;

            try
            {
                descplanoDAO.Gravar(descPlano);
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
            ViewBag.DescricaoPlanoId = new MultiSelectList(descplanoDAO.BuscarTodos(), "descricaoId", "descricao");
            DescricaoPlano descPlano = descplanoDAO.Buscar(id);
            return PartialView("~/Views/DescricaoPlano/_form.cshtml", descPlano);
        }
        [HttpPost]
        public ActionResult Editar(DescricaoPlano descPlano)
        {
            var result = false;

            try
            {
                descplanoDAO.Alterar(descPlano);
                result = true;

            }
            catch (Exception ex)
            {
                GravarLog("DescricaoPlano", "Erro ao Gravar Descrição : " + ex.Message, 1);
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
                var m = descplanoDAO.Buscar(id);
                descplanoDAO.Excluir(m);
                result = true;
            }
            catch (Exception ex)
            {
                //TODO: LOG 
                GravarLog("DescricaoPlano", "Erro ao Excluir Descricao : " + ex.Message, 1);
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}