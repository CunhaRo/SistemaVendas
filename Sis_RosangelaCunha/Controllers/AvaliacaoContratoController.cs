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
    public class AvaliacaoContratoController : Controller
    {
        private readonly AvaliacaoContratoDAO avaliacaoDAO = new AvaliacaoContratoDAO();

        #region Novo
        public ActionResult Novo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Novo(AvaliacaoContrato avaliacao)
        {
            if (ModelState.IsValid)
            {
                avaliacaoDAO.Gravar(avaliacao);
                return RedirectToAction("Listar");
            }
            return View(avaliacao);
        }
        #endregion
        #region Listar
        public ActionResult Listar()
        {
            IList<AvaliacaoContrato> avaliacao = avaliacaoDAO.BuscarTodos();
            return View(avaliacao);
        }
        #endregion
        #region Editar
        public ActionResult Editar(int id)
        {
            AvaliacaoContrato avaliacao = avaliacaoDAO.Buscar(id);
            return View(avaliacao);
        }
        [HttpPost]
        public ActionResult Editar(AvaliacaoContrato avaliacao)
        {
                avaliacaoDAO.Alterar(avaliacao);
                return RedirectToAction("Listar");
        }
        #endregion
        #region Excluir
        //public ActionResult Excluir(int id)
        //{
        //    AvaliacaoContrato avaliacao = avaliacaoDAO.Buscar(id);
        //    return View(avaliacao);
        //}
        //[HttpPost]
        //public ActionResult Excluir(AvaliacaoContrato objAvaliacao)
        //{
        //    avaliacaoDAO.Excluir(objAvaliacao);
        //    return RedirectToAction("Listar");
        //}
        #endregion
    }
}