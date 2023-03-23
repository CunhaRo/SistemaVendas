using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sis_RosangelaCunha.Autenticacao;
using Sis_RosangelaCunha.DAO;
using Sis_RosangelaCunha.Models;
using System.IO;

namespace Sis_RosangelaCunha.Controllers
{
     //[Autorizar]
    public class ItemAnexadoController : Controller
    {
        private readonly ItemAnexadoDAO itemDAO = new ItemAnexadoDAO();
        private readonly AquisicaoContratoDAO aquisicaoDAO = new AquisicaoContratoDAO();

        #region Novo
        public ActionResult Novo(int aquisicaoId)
        {
            return View(aquisicaoId);
        }
        [HttpPost]
        public ActionResult Novo(ItemAnexado item, int aquisicaoId)
        {
            if (ModelState.IsValid)
            {
                item.objAquisicao.AquisicaoId = aquisicaoId;
                itemDAO.Inserir(item);
                return RedirectToAction("Listar");
            }
            return View(item);
        }
        #endregion
        #region Listar
        public ActionResult Listar()
        {
            IList<ItemAnexado> item = itemDAO.BuscarTodos();
            return View(item);
        }
        #endregion
        #region Editar
        public ActionResult Editar(int id)
        {
            ItemAnexado item = itemDAO.Buscar(id);
            ViewBag.aquisicaoId = new SelectList(aquisicaoDAO.BuscarTodos(), "aquisicaoId", "dataAquisicao");
            return View(item);
        }
        [HttpPost]
        public ActionResult Editar(ItemAnexado item, int aquisicaoId)
        {
            if (ModelState.IsValid)
            {
                item.objAquisicao.AquisicaoId = aquisicaoId;
                itemDAO.Alterar(item);
                return RedirectToAction("Listar");
            }
            ViewBag.aquisicaoId = new SelectList(aquisicaoDAO.BuscarTodos(), "aquisicaoId", "dataAquisicao");
            return View(item);
        }
        #endregion
        #region Excluir
        public ActionResult Excluir(int id)
        {
            ItemAnexado item = itemDAO.Buscar(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Excluir(ItemAnexado objItem)
        {
            itemDAO.Excluir(objItem);
            return RedirectToAction("Listar");
        }
        #endregion

        public ActionResult Upload()
        {
            int arquivosSalvos = 0;
            for (int i = 0;i <Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];
                //Valivadções...
                if(arquivo.ContentLength > 0)
                {
                    var upload = Server.MapPath("~/Content/Uploads");
                    string caminhoArquivo = Path.Combine(@upload, Path.GetFileName(arquivo.FileName));
                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }

            }
            ViewData["Message"] = String.Format("{0} Arquivos(s) salvo(s) com sucesso.", arquivosSalvos);
            return View("Upload");
        }


        [HttpPost]
        public ActionResult SaveFiles(string Id)
        {
            var context = this.HttpContext.ApplicationInstance.Context;

            context.Response.ContentType = "text/plain";

            bool isSavedSuccessfully = true;

            string fName = "";

            foreach (string fileName in HttpContext.Request.Files)
            {
                var file = Request.Files[fileName];
                fName = file.FileName;

                if (file != null && file.ContentLength > 0)
                {
                    var basedir = ConfigurationManager.AppSettings["basedir"].ToString();
                    var originalDirectory = new DirectoryInfo(string.Format("{0}{1}", Server.MapPath(@"\Content\"), basedir));


                    string pathString = Path.Combine(originalDirectory.ToString(), "imagepath");
                    var fileName1 = Path.GetFileName(file.FileName);
                    bool isExists = Directory.Exists(pathString);

                    if (!isExists)
                        Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, file.FileName);

                    try
                    {
                        file.SaveAs(path);

                        var _repo = new ItemAnexadoDAO();
                        _repo.InserirArquivoEmpresa(path, Convert.ToInt32(Id), file.ContentType);
                        
                        System.IO.File.Delete(path);
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("Erro ao salvar arquivo | Mensagem : {0}", ex.Message);

                        //InserirLog("ARQUIVO EMPRESA", message);

                        isSavedSuccessfully = false;
                    }
                }
            }

            if (isSavedSuccessfully)
            {
                //InserirLog("ARQUIVO EMPRESA", "Arquivo gravado", "Sucesso");
                return Json(new { Message = fName });
            }
            else
            {
                //InserirLog("ARQUIVO EMPRESA", "Erro ao salvar arquivo");
                return Json(new { Message = "Erro ao salvar arquivo" });
            }
        }


        /// <summary>
        /// Get Upload File
        /// </summary>
        /// <param name="idArquivo">id do Arquivo</param>
        /// <returns></returns>
        public FileContentResult GetUploadedFile(int idArquivo)
        {
            var arquivo = new ItemAnexado();

            var _repo = new ItemAnexadoDAO();
                arquivo = _repo.Buscar(idArquivo);
            
            return File(arquivo.Arquivo, arquivo.MimeType, arquivo.Nome);
        }
    }
}