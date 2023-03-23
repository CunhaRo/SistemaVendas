using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sis_RosangelaCunha.Autenticacao;
using Sis_RosangelaCunha.DAO;
using Sis_RosangelaCunha.Models;
using Sis_RosangelaCunha.ViewModel;


namespace Sis_RosangelaCunha.Controllers
{

    public class AgendamentoController : BaseController
    {
        private readonly AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
        private readonly ClienteDAO clienteDAO = new ClienteDAO();
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        
        public ActionResult Index()
        {
            return View();
        }

        #region Novo

        public ActionResult Novo()
        {
            var agendamentoViewModel = new AgendamentoViewModel();
            agendamentoViewModel.IdUsuario = IdUsuario;
            return PartialView("~/Views/Agendamento/_form.cshtml", agendamentoViewModel);
        }

        [HttpPost]
        public ActionResult Novo(AgendamentoViewModel agendamentoVM)
        {
            var result = false;

            if (ModelState.IsValid)
            {
                Agendamento agendamento = new Agendamento();

                agendamento.objCliente = clienteDAO.Buscar(agendamentoVM.IdCliente);
                agendamento.objUsuario = usuarioDAO.Buscar(agendamentoVM.IdUsuario);
                agendamento.HorarioAgendamento = agendamentoVM.HorarioAgendamento;
                agendamento.Indicacao = agendamentoVM.Indicacao;
                agendamento.IndicadoPor = agendamentoVM.IndicadoPor;
                agendamento.Status = agendamentoVM.Status;
                agendamentoDAO.Gravar(agendamento);

                result = true;
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Listar
        public ActionResult Listar(string busca)
        {

            var agendamento = agendamentoDAO.BuscarPorData(busca).ToList();

            return PartialView("~/Views/Agendamento/_listar.cshtml", agendamento);
        }


        /// <summary>
        /// Listar Municipios por Estado
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarJson()
        {
            var lista = new AgendamentoViewModel().CarregaAgendamento();
            return Json(new { Resultado = lista }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Editar
        public ActionResult Editar(int agendamentoId)
        {
            Agendamento agendamento = agendamentoDAO.Buscar(agendamentoId);
            ViewBag.clienteId = new SelectList(clienteDAO.BuscarFantasia(""), "clienteId", "fantasia", agendamento.objCliente.ClienteId);
            ViewBag.usuarioId = new SelectList(usuarioDAO.BuscarTodos(), "usuarioId", "nome", agendamento.objUsuario.UsuarioId);
            return View(agendamento);
        }
        [HttpPost]
        public ActionResult Editar(Agendamento agendamento, int clienteId, int usuarioId)
        {
            if (ModelState.IsValid)
            {
                agendamento.objCliente.ClienteId = clienteId;
                agendamento.objUsuario.UsuarioId = usuarioId;
                agendamentoDAO.Alterar(agendamento);
                return RedirectToAction("Listar");
            }
            ViewBag.clienteId = new SelectList(clienteDAO.BuscarFantasia(""), "clienteId", "fantasia", clienteId);
            ViewBag.usuarioIdId = new SelectList(usuarioDAO.BuscarTodos(), "usuarioId", "nome", usuarioId);

            return View(agendamento);
        }
        #endregion
        #region Excluir
        public ActionResult Excluir(int agendamentoId)
        {
            Agendamento agendamento = agendamentoDAO.Buscar(agendamentoId);
            return View(agendamento);
        }
        [HttpPost]
        public ActionResult Excluir(Agendamento objAgendamento)
        {
            agendamentoDAO.Excluir(objAgendamento);
            return RedirectToAction("Listar");
        }
        #endregion

    }
}