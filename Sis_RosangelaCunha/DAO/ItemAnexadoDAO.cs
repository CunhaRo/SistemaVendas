using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Sis_RosangelaCunha.DAO
{
    public class ItemAnexadoDAO
    {
        #region Gravar
        public void Inserir(ItemAnexado item)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into ItensAnexados(Nome,MimeType,Arquivo,AquisicaoId) values (@nome,@mimetype,@arquivo,@aquisicaoId)";
            comando.Parameters.AddWithValue("@nome", item.Nome);
            comando.Parameters.AddWithValue("@arquivo", item.Arquivo);
            comando.Parameters.AddWithValue("@mimetype", item.MimeType);
            comando.Parameters.AddWithValue("@aquisicaoId", item.objAquisicao.AquisicaoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Alterar(ItemAnexado item)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update ItensAnexados set NomeArquivo=@nomeArquivo,AquisicaoId=@aquisicaoId where ItensId=@itensId";
            //comando.Parameters.AddWithValue("@nomeArquivo", item.NomeArquivo);
            //comando.Parameters.AddWithValue("@tamanho", item.Tamanho);
            //comando.Parameters.AddWithValue("@tipo", item.Tipo);
            //comando.Parameters.AddWithValue("@caminho", item.Caminho);
            comando.Parameters.AddWithValue("@aquisicaoId", item.objAquisicao.AquisicaoId);
            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(ItemAnexado item)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from ItensAnexados where ItensId=@itensId";
            comando.Parameters.AddWithValue("@itensId", item.ItensId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<ItemAnexado> BuscarTodos()
        {
            IList<ItemAnexado> listaItens = new List<ItemAnexado>();
            AquisicaoContratoDAO aquisicaoDAO = new AquisicaoContratoDAO();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From ItensAnexados";
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ItemAnexado objItem = new ItemAnexado();

                    objItem.ItensId = Convert.ToInt32(dr["ItensId"]);
                    //objItem.NomeArquivo = dr["NomeArquivo"].ToString();
                    objItem.objAquisicao = aquisicaoDAO.Buscar((int)dr["AquisicaoId"]);


                    listaItens.Add(objItem);
                }
            }
            else
            {
                listaItens = null;
            }
            dr.Close();
            return listaItens;
        }
        #endregion



        /// <summary>
        /// Inserir Arquivo
        /// </summary>
        /// <param name="file">Nome do arquivo </param>
        /// <param name="id">Id do arquivo</param>
        public bool InserirArquivoEmpresa(string file, int id, string mimetype)
        {
            var result = false;

            if (string.IsNullOrEmpty(file) || id == 0) return result;

            var local = file;

            var uploaddir = ConfigurationManager.AppSettings["basedir"].ToString();

            var idreplace = string.Format("{0}-", id);

            var nome = Path.GetFileName(file).Replace(idreplace, "");

            try
            {
                var fi = new FileInfo(file);
                var fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
                var rd = new BinaryReader(fs);
                var filedata = rd.ReadBytes((int)fs.Length);

                rd.Close();
                fs.Close();

                var arquivo = new ItemAnexado();

                arquivo.Nome = nome;
                
                arquivo.MimeType = mimetype;
                arquivo.Arquivo = filedata;
                arquivo.objAquisicao = new AquisicaoContratoDAO().Buscar(id);

                Inserir(arquivo);

            }
            catch (Exception ex)
            {
                string message = "Erro ao incluir arquivo empresa - Message = " + ex.Message;
                //InserirLog("INCLUIR ARQUIVO EMPRESA", message);
            }

            return result;
        }

        public ItemAnexado RecuperarArquivoEmpresa(int idArquivo)
        {
            var arquivo = Buscar(idArquivo);
            return arquivo;
        }




        #region Buscar
        public ItemAnexado Buscar(int id)
        {
            AquisicaoContratoDAO objAquisicaoDAO = new AquisicaoContratoDAO();
            ItemAnexado objItem = new ItemAnexado();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From ItensAnexados where requerimentoId = @requerimentoId";
            comando.Parameters.AddWithValue("@ItensId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();


                objItem.ItensId = Convert.ToInt32(dr["ItensId"]);
                objItem.Nome = dr["Nome"].ToString();
                objItem.objAquisicao = objAquisicaoDAO.Buscar((int)dr["AquisicaoId"]);
            }
            else
            {
                objItem = null;
            }
            dr.Close();
            return objItem;

        #endregion
        }

    }
}