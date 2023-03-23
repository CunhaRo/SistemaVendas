using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Sis_RosangelaCunha.DAO
{
    public class ConexaoBanco
    {
        public static SqlConnection Conectar()
        {
            string stringConexao = ConfigurationManager.ConnectionStrings["Sicov"].ConnectionString;
            SqlConnection conexao = new SqlConnection(stringConexao);
            conexao.Open();
            return conexao;

        }
        public static void CRUD(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            comando.ExecuteNonQuery();
            con.Close();
        }
        public static SqlDataReader Selecionar(SqlCommand comando)
        {
            SqlConnection con = Conectar();
            comando.Connection = con;
            SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
    }
}