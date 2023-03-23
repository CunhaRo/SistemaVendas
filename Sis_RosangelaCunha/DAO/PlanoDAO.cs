using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;

namespace Sis_RosangelaCunha.DAO
{
    public class PlanoDAO
    {
        #region Gravar
        public void Gravar(Plano plano)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into Planos(NomePlano,Valor,Mensalidades,DescricaoId) values (@nomePlano,@valor,@mensalidades,@descricaoId)";
            comando.Parameters.AddWithValue("@nomePlano", plano.NomePlano);
            comando.Parameters.AddWithValue("@valor", plano.Valor);
            comando.Parameters.AddWithValue("@mensalidades", plano.Mensalidades);
            comando.Parameters.AddWithValue("@descricaoId", plano.ObjDescricaoPlano.DescricaoId);


            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Alterar(Plano plano)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update Planos set NomePlano=@nomePlano,Valor=@valor,Mensalidades=@mensalidades,DescricaoId=@descricaoId where PlanoId=@planoId";
            comando.Parameters.AddWithValue("@nomePlano", plano.NomePlano);
            comando.Parameters.AddWithValue("@valor", plano.Valor);
            comando.Parameters.AddWithValue("@mensalidades", plano.Mensalidades);
            comando.Parameters.AddWithValue("@descricaoId", plano.ObjDescricaoPlano.DescricaoId);
            comando.Parameters.AddWithValue("@planoId", plano.PlanoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(Plano plano)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from Planos where PlanoId=@planoId";
            comando.Parameters.AddWithValue("@planoId", plano.PlanoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<Plano> BuscarTodos()
        {
            IList<Plano> listaPlanos = new List<Plano>();
            DescricaoPlanoDAO descricaoPlanoDao = new DescricaoPlanoDAO();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Planos";
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Plano objPlano = new Plano();

                    objPlano.PlanoId = Convert.ToInt32(dr["PlanoId"]);
                    objPlano.NomePlano = dr["NomePlano"].ToString();
                    objPlano.Valor = dr["Valor"].ToString();
                    objPlano.Mensalidades = (enumMensalidades)Enum.Parse(typeof(enumMensalidades), dr["Mensalidades"].ToString());
                    objPlano.ObjDescricaoPlano = descricaoPlanoDao.Buscar((int)dr["DescricaoId"]);
                    //objPlano.MensalidadeConsumacao = Convert.ToBoolean(dr["MensalidadeConsumacao"]);
                    //objPlano.Mensalidade = Convert.ToBoolean(dr["Mensalidades"]);

                    listaPlanos.Add(objPlano);
                }
            }
           
            dr.Close();
            return listaPlanos;
        }
        #endregion

        #region Buscar
        public Plano Buscar(int id)
        {
            Plano objPlano = new Plano();
            DescricaoPlanoDAO desPlanoDao = new DescricaoPlanoDAO();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Planos where PlanoId = @planoId";
            comando.Parameters.AddWithValue("@planoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objPlano.PlanoId = Convert.ToInt32(dr["PlanoId"]);
                objPlano.NomePlano = dr["NomePlano"].ToString();
                objPlano.Valor = dr["Valor"].ToString();
                objPlano.Mensalidades = (enumMensalidades)Enum.Parse(typeof(enumMensalidades), dr["Mensalidades"].ToString());
                objPlano.ObjDescricaoPlano = desPlanoDao.Buscar((int)dr["DescricaoId"]);

            }
            else
            {
                objPlano = null;
            }
            dr.Close();
            return objPlano;

        #endregion
        }
        #region BuscarDescricao
        public IList<Plano> BuscarPorDescricao(string nomePlano)
        {
            IList<Plano> listaPlano = new List<Plano>();
            DescricaoPlanoDAO descricaoPlanoDao = new DescricaoPlanoDAO();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM Planos Where NomePlano LIKE @nomePlano";

            comando.Parameters.AddWithValue("@nomePlano", string.Format("%{0}%", nomePlano));

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Plano objPlano = new Plano();

                    objPlano.PlanoId = Convert.ToInt32(dr["PlanoId"]);
                    objPlano.NomePlano = dr["NomePlano"].ToString();
                    objPlano.Valor = dr["Valor"].ToString();
                    objPlano.Mensalidades = (enumMensalidades)Enum.Parse(typeof(enumMensalidades), dr["Mensalidades"].ToString());
                    objPlano.ObjDescricaoPlano = descricaoPlanoDao.Buscar((int)dr["DescricaoId"]);


                    listaPlano.Add(objPlano);
                }
            }
            else
            {
                listaPlano = null;
            }
            return listaPlano;
        }
        #endregion
    }
}