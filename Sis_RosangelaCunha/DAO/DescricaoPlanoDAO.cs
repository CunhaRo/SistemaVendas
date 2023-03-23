using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;

namespace Sis_RosangelaCunha.DAO
{
    public class DescricaoPlanoDAO
    {
        #region Gravar
        public void Gravar(DescricaoPlano descplano)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into DescricoesPlano(DescricaoId,Descricao) values (@descricaoId,@descricao)";
            comando.Parameters.AddWithValue("@descricao", descplano.Descricao);


            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Alterar(DescricaoPlano descplano)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update DescricoesPlano set Descricao=@descricao where DescricaoId=@descricaoId";
            comando.Parameters.AddWithValue("@descricao", descplano.Descricao);
            comando.Parameters.AddWithValue("@descricaoId", descplano.DescricaoId);


            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(DescricaoPlano descplano)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from DescricoesPlano where DescricaoId=@descricaoId";
            comando.Parameters.AddWithValue("@descricaoId", descplano.DescricaoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<DescricaoPlano> BuscarTodos()
        {
            IList<DescricaoPlano> listaDescricaoPlanos = new List<DescricaoPlano>();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From DescricoesPlano";
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DescricaoPlano objDescricaoPlano = new DescricaoPlano();

                    objDescricaoPlano.DescricaoId = Convert.ToInt32(dr["DescricaoId"]);
                    objDescricaoPlano.Descricao = dr["Descricao"].ToString();

                    listaDescricaoPlanos.Add(objDescricaoPlano);
                }
            }
            
            dr.Close();
            return listaDescricaoPlanos;
        }
        #endregion

        #region Buscar
        public DescricaoPlano Buscar(int id)
        {
            DescricaoPlano objDescricaoPlano = new DescricaoPlano();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From DescricoesPlano where DescricaoId = @descricaoId";
            comando.Parameters.AddWithValue("@descricaoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();


                objDescricaoPlano.DescricaoId = Convert.ToInt32(dr["DescricaoId"]);
                objDescricaoPlano.Descricao = dr["Descricao"].ToString();

            }
            else
            {
                objDescricaoPlano = null;
            }
            dr.Close();
            return objDescricaoPlano;

            #endregion
        }
        #region BuscarDescricao
        public IList<DescricaoPlano> BuscarPorDescricao(string descPlano)
        {
            IList<DescricaoPlano> listaDescricaoPlanos = new List<DescricaoPlano>();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM DescricoesPlano Where Descricao LIKE @descricao";

            comando.Parameters.AddWithValue("@descricao", string.Format("%{0}%", descPlano));

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DescricaoPlano objDescricaoPlano = new DescricaoPlano();

                    objDescricaoPlano.DescricaoId = Convert.ToInt32(dr["DescricaoId"]);
                    objDescricaoPlano.Descricao = dr["Descricao"].ToString();


                    listaDescricaoPlanos.Add(objDescricaoPlano);
                }
            }
            else
            {
                listaDescricaoPlanos = null;
            }
            return listaDescricaoPlanos;
        }
        #endregion
    }
}