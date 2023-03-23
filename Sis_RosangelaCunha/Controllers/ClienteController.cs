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
    public class ClienteController : BaseController
    {
        #region Variaveis
        private readonly ClienteDAO clienteDAO = new ClienteDAO();
       
        #endregion

        #region Actions

        public ActionResult Index()
        {
            return View();

        }


        /// <summary>
        /// Listar
        /// </summary>
        /// <returns></returns>
        public ActionResult Listar(string busca)
        {
            ViewBag.fantasia = "fantasia";

            var clientes = new List<Cliente>();

            if(string.IsNullOrEmpty(busca))
            {
                clientes = clienteDAO.BuscarTodos().ToList();
            }
            else
            {
                clientes = clienteDAO.BuscarFantasia(busca).ToList();
            }

            clientes = clientes.OrderBy(s => s.NomeFantasia).ToList();
            return PartialView("~/Views/Cliente/_listar.cshtml", clientes);
        }

        /// <summary>
        /// Novo
        /// </summary>
        /// <returns></returns>
        public ActionResult Novo()
        {
            var cliente = new Cliente();
            return PartialView("~/Views/Cliente/_form.cshtml", cliente);
        }

        /// <summary>
        /// Novo
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Novo(Cliente cliente)
        {
            var result = false;

            try
            {
                clienteDAO.Gravar(cliente);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Editar(int id)
        {
            ViewBag.clienteId = new MultiSelectList(clienteDAO.BuscarTodos(), "clienteId", "fantasia");
            Cliente cliente = clienteDAO.Buscar(id);
            return PartialView("~/Views/Cliente/_form.cshtml", cliente);
        }


        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editar(Cliente cliente)
        {
            var result = false;

            try
            {
                clienteDAO.Editar(cliente);
                result = true;

            }
            catch (Exception ex)
            {
                GravarLog("Cliente", "Erro ao Gravar Dados Cliente : " + ex.Message, 1);
                result = false;
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            var result = false;
            try
            {
                var m = clienteDAO.Buscar(id);
                clienteDAO.Excluir(m);
                result = true;
            }
            catch (Exception ex)
            {
                //TODO: LOG 
                GravarLog("Cliente", "Erro ao Excluir Cliente : " + ex.Message, 1);
            }

            return Json(new { Resultado = result }, JsonRequestBehavior.AllowGet);

        }

        #endregion
        //#region Fantasia
        //public ActionResult ListarPorFantasia(string fantasia)
        //{
        //    return PartialView("ListarPorFantasia", clienteDAO.BuscarFantasia(fantasia));
        //}
        //#endregion
        //#region RazaoSocial
        //public ActionResult ListarPorRazao(string razaoSocial)
        //{
        //    return PartialView("ListarPorRazao", clienteDAO.BuscarPorRazao(razaoSocial));
        //}
        //#endregion


        public ActionResult NovoClienteSimples(string nome)
        {

            //TODO arrumar a baga;a

            return Json(new {Resultado = true}, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Listar Municipios por Estado
        /// </summary>
        /// <returns></returns>
        public ActionResult ListarClientes()
        {
            var lista = new AquisicaoContratoViewModel().CarregaClientes();
            return Json(new { Resultado = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}