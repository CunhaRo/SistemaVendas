using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;

namespace Sis_RosangelaCunha.DAO
{
    public class AvaliacaoContratoDAO
    {
        #region Gravar
        public void Gravar(AvaliacaoContrato avaliacao)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into AvaliacaoContrato(Contato,DataContato,Comentarios,Features,Treinamento,Atendimento) values (@contato,@dataContato,@comentarios,@features,@treinamento,@atendimento)";
            comando.Parameters.AddWithValue("@contato", avaliacao.Contato);
            comando.Parameters.AddWithValue("@dataContato", avaliacao.DataContato);
            comando.Parameters.AddWithValue("@comentario", avaliacao.Comentarios);
            comando.Parameters.AddWithValue("@features", avaliacao.Features);  
            comando.Parameters.AddWithValue("@treinamento", avaliacao.Treinamento);
            comando.Parameters.AddWithValue("@atendimento", avaliacao.Atendimento);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Alterar(AvaliacaoContrato avaliacao)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update AvaliacaoContrato set Contato=@contato,DataContato=@dataContato,Comentarios=@comentarios,Features=@features,Treinamento=@treinamento,Atendimento=@atendimento Where AvaliacaoId=@avaliacaoId";
            comando.Parameters.AddWithValue("@contato", avaliacao.Contato);
            comando.Parameters.AddWithValue("@dataContato", avaliacao.DataContato);
            comando.Parameters.AddWithValue("@comentario", avaliacao.Comentarios);
            comando.Parameters.AddWithValue("@features", avaliacao.Features);
            comando.Parameters.AddWithValue("@treinamento", avaliacao.Treinamento);
            comando.Parameters.AddWithValue("@atendimento", avaliacao.Atendimento);
            comando.Parameters.AddWithValue("@avaliacao", avaliacao.AvaliacaoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(AvaliacaoContrato avaliacao)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from AvaliacaoContrato where AvaliacaoId=@avaliacaoId";
            comando.Parameters.AddWithValue("@avaliacaoId", avaliacao.AvaliacaoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<AvaliacaoContrato> BuscarTodos()
        {
            IList<AvaliacaoContrato> listaAvaliacoes = new List<AvaliacaoContrato>();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From AvaliacaoContrato";
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    AvaliacaoContrato objAvaliacao = new AvaliacaoContrato();

                    objAvaliacao.AvaliacaoId = Convert.ToInt32(dr["avaliacaoId"]);
                    objAvaliacao.Contato = dr["Contato"].ToString();
                    objAvaliacao.DataContato = Convert.ToDateTime(dr["DataContato"]);
                    objAvaliacao.Comentarios = dr["comentario"].ToString();
                    objAvaliacao.Features = Convert.ToBoolean(dr["features"]);
                    objAvaliacao.Treinamento = Convert.ToBoolean(dr["treinamento"]);
                    objAvaliacao.Atendimento = dr["Atendimento"].ToString();
                    listaAvaliacoes.Add(objAvaliacao);
                }
            }
            else
            {
                listaAvaliacoes = null;
            }
            dr.Close();
            return listaAvaliacoes;
        }
        #endregion

        #region Buscar
        public AvaliacaoContrato Buscar(int id)
        {
            AvaliacaoContrato objAvaliacao = new AvaliacaoContrato();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From AvaliacaoContrato where AvaliacaoId = @avaliacaoId";
            comando.Parameters.AddWithValue("@avaliacaoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objAvaliacao.AvaliacaoId = Convert.ToInt32(dr["avaliacaoId"]);
                objAvaliacao.Contato = dr["Contato"].ToString();
                objAvaliacao.DataContato = Convert.ToDateTime(dr["DataContato"]);
                objAvaliacao.Comentarios = dr["comentario"].ToString();
                objAvaliacao.Features = Convert.ToBoolean(dr["features"]);
                objAvaliacao.Treinamento = Convert.ToBoolean(dr["treinamento"]);
                objAvaliacao.Atendimento = dr["Atendimento"].ToString();
            }
            else
            {
                objAvaliacao = null;
            }
            dr.Close();
            return objAvaliacao;

        #endregion
        }
    }
}