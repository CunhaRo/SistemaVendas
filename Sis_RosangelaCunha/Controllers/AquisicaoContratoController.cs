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
    public class AquisicaoContratoController : Controller
    {
        private readonly AquisicaoContratoDAO aquisicaoDAO = new AquisicaoContratoDAO();
        private readonly ClienteDAO clienteDAO = new ClienteDAO();
        private readonly PlanoDAO planoDAO = new PlanoDAO();
        //private readonly AvaliacaoContratoDAO avaliacaoDAO = new AvaliacaoContratoDAO();
        //private readonly AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
        private readonly UsuarioDAO usuarioDAO = new UsuarioDAO();
        private readonly ItemAnexadoDAO anexoDAO = new ItemAnexadoDAO();

        public ActionResult Index()
        {
            return View();

        }
        #region Novo
        public ActionResult Novo(int idAgendamento, bool semagendamento)
        {
            //ViewBag.ItemAnexado = new MultiSelectList(new ItemAnexadoDAO().BuscarTodos(), "requerimentoId", "nomeArquivo");
            //ViewBag.clienteId = new SelectList(clienteDAO.BuscarFantasia(""), "clienteId", "fantasia");
            //ViewBag.planoId = new SelectList(planoDAO.BuscarTodos(), "planoId", "descPlano");
           // ViewBag.avaliacaoId = new SelectList(avaliacaoDAO.BuscarTodos(), "avaliacaoId", "contato");
            //ViewBag.agendamentoId = new SelectList(agendamentoDAO.BuscarTodos(), "agendamentoId", "dataAgendamento"); //Confirmar se ele vai ser necessario
            //ViewBag.usuarioId = new SelectList(usuarioDAO.BuscarTodos(), "usuarioId", "nome");
            var aquisicaoContratoViewModel = new AquisicaoContratoViewModel(idAgendamento, semagendamento);

            return PartialView("~/Views/AquisicaoContrato/_form.cshtml", aquisicaoContratoViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aquisicaoVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Novo(AquisicaoContratoViewModel aquisicaoVM)
        {
            var result = false;

            if (ModelState.IsValid)
            {
                AquisicaoContrato aquisicao = new AquisicaoContrato();
                 
                aquisicao.objCliente = clienteDAO.Buscar(aquisicaoVM.IdCliente);
                aquisicao.objPlano = planoDAO.Buscar(aquisicaoVM.IdPlano);
                //aquisicao.objAvaliacao.AvaliacaoId = avalicaoId;
                //aquisicao.objAgendamento.AgendamentoId = agendamentoId;
                //aquisicao.objUsuario.UsuarioId = usuarioId;
                aquisicaoDAO.Gravar(aquisicao);
                result = true;
                return RedirectToAction("Listar");
            }
            return Json(new {Resultado = result}, JsonRequestBehavior.AllowGet);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        #region Listar
        public ActionResult Listar(string busca)
        {
            
            var aquisicoes = aquisicaoDAO.BuscarporTermo(busca).ToList();
           
            return PartialView("~/Views/AquisicaoContrato/_listar.cshtml", aquisicoes);


        }
        #endregion
        #region Editar
        public ActionResult Editar(int aquisicaoId)
        {
            
            AquisicaoContrato aquisicao = aquisicaoDAO.Buscar(aquisicaoId);
            ViewBag.clienteId = new SelectList(clienteDAO.BuscarFantasia(""), "clienteId", "fantasia", aquisicao.objCliente.ClienteId);
            ViewBag.planoId = new SelectList(planoDAO.BuscarTodos(), "planoId", "descPlano", aquisicao.objPlano.PlanoId);
            //ViewBag.avaliacaoId = new SelectList(avaliacaoDAO.BuscarTodos(), "avaliacaoId", "contato", aquisicao.objAvaliacao.AvaliacaoId);
            //ViewBag.agendamentoId = new SelectList(agendamentoDAO.BuscarTodos(), "agendamentoId", "dataAgendamento", aquisicao.objAgendamento.AgendamentoId); //Confirmar se ele vai ser necessario
            //ViewBag.usuarioId = new SelectList(usuarioDAO.BuscarTodos(), "usuarioId", "nome", aquisicao.objUsuario.UsuarioId);
            return View(aquisicao);
           
        }
        [HttpPost]
        public ActionResult Editar(AquisicaoContrato aquisicao, int clienteId, int planoId, int avaliacaoId, int agendamentoId, int usuarioId)
        {
        if (ModelState.IsValid)
            {
                aquisicao.objCliente.ClienteId = clienteId;
            aquisicao.objPlano.PlanoId = planoId;
            aquisicao.objAvaliacao.AvaliacaoId = avaliacaoId;
            aquisicao.objAgendamento.AgendamentoId = agendamentoId;
            aquisicao.objUsuario.UsuarioId = usuarioId;
            aquisicaoDAO.Alterar(aquisicao);
            return RedirectToAction("Listar");
            }
            ViewBag.clienteId = new SelectList(clienteDAO.BuscarFantasia(""), "clienteId", "fantasia", clienteId);
            ViewBag.planoId = new SelectList(planoDAO.BuscarTodos(), "planoId", "descPlano", planoId);
            //ViewBag.avaliacaoId = new SelectList(avaliacaoDAO.BuscarTodos(), "avaliacaoId", "contato", avaliacaoId);
            //ViewBag.agendamentoId = new SelectList(agendamentoDAO.BuscarTodos(), "agendamentoId", "dataAgendamento", agendamentoId); //Confirmar se ele vai ser necessario
            //ViewBag.usuarioId = new SelectList(usuarioDAO.BuscarTodos(), "usuarioId", "nome", usuarioId);
            return View(aquisicao);
        }
        #endregion
        #region Excluir
        public ActionResult Excluir(int aquisicaoId)
        {
            AquisicaoContrato aquisicao = aquisicaoDAO.Buscar(aquisicaoId);
            return View(aquisicao);
        }
        [HttpPost]
        public ActionResult Excluir(AquisicaoContrato objAquisicao)
        {
            aquisicaoDAO.Excluir(objAquisicao);
            return RedirectToAction("Listar");
        }
        #endregion


        public ActionResult Verificar()
        {
            return PartialView("_agendamento");
        }
    }
}