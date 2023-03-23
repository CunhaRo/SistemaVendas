using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Sis_RosangelaCunha.DAO
{
    public class AgendamentoDAO
    {
        #region Gravar
        public void Gravar(Agendamento agendamento)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into Agendamentos(horarioAgendamento,indicacao,enumStatus,clienteId,usuarioId) values (@horarioAgendamento,@indicacao,@status,@clienteId,@usuarioId)";
            comando.Parameters.AddWithValue("@horarioAgendamento", agendamento.HorarioAgendamento);
            comando.Parameters.AddWithValue("@indicacao", agendamento.Indicacao);
            comando.Parameters.AddWithValue("@status", (int)agendamento.Status);
            comando.Parameters.AddWithValue("@clienteId", agendamento.objCliente.ClienteId);
            comando.Parameters.AddWithValue("@usuarioId", agendamento.objUsuario.UsuarioId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Alterar(Agendamento agendamento)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update Agendamentos set horarioAgendamento=@horarioAgendamento,indicacao=@indicacao,status=@status,cliente=@clienteId,usuario=usuarioId where agendamentoId=@agendamentoId";
            comando.Parameters.AddWithValue("@agendamentoId", agendamento.AgendamentoId);
            comando.Parameters.AddWithValue("@horarioAgendamento", agendamento.HorarioAgendamento);
            comando.Parameters.AddWithValue("@indicacao", agendamento.Indicacao);
            comando.Parameters.AddWithValue("@status", agendamento.Status);
            comando.Parameters.AddWithValue("@clienteId", agendamento.objCliente.ClienteId);
            comando.Parameters.AddWithValue("@usuarioId", agendamento.objUsuario.UsuarioId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(Agendamento agendamento)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from Agendamentos where agendamentoId=@agendamentoId";
            comando.Parameters.AddWithValue("@agendamentoId", agendamento.AgendamentoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        public IList<Agendamento> BuscarPorData(string busca)
        {
            IList<Agendamento> listaAgendamento = new List<Agendamento>();
            ClienteDAO clienteDAO = new ClienteDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            Agendamento objAgendamento = new Agendamento();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;

            var builder = new StringBuilder();

            builder.Append("Select a.* From Agendamentos a");


            if (!string.IsNullOrEmpty(busca))
            {
                builder.Append(" inner join Clientes c on a.clienteId = c.clienteId ");
                builder.AppendFormat(" where  c.Nome like '{0}'", busca);
            }

            comando.CommandText = builder.ToString();



            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            while (dr.Read())
            {
                objAgendamento.HorarioAgendamento = Convert.ToDateTime(dr["HorarioAgendamento"]);
                objAgendamento.Indicacao = Convert.ToBoolean(dr["Indicacao"]);
                objAgendamento.IndicadoPor = dr["IndicadoPor"].ToString();
                objAgendamento.Status = (Status)Enum.Parse(typeof(Status), dr["enumStatus"].ToString());
                objAgendamento.objUsuario = usuarioDAO.Buscar((int)dr["usuarioId"]);
                objAgendamento.objCliente = clienteDAO.Buscar((int)dr["clienteId"]);

                listaAgendamento.Add(objAgendamento);
            }

            ///dr.Close();

            return listaAgendamento;
        }
        #region BuscarTodos

        public IList<Agendamento> BuscarTodos()
        {
            ClienteDAO clienteDAO = new ClienteDAO();

            UsuarioDAO usuarioDAO = new UsuarioDAO();

            IList<Agendamento> listaAgendamento = new List<Agendamento>();

            SqlCommand comando = new SqlCommand();

            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Agendamentos";

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Agendamento objAgendamento = new Agendamento();

                    objAgendamento.HorarioAgendamento = Convert.ToDateTime(dr["horarioAgendamento"]);
                    objAgendamento.Indicacao = Convert.ToBoolean(dr["indicacao"]);
                    objAgendamento.Status = (Status)Enum.Parse(typeof(Status), dr["enumStatus"].ToString());
                    objAgendamento.objCliente = clienteDAO.Buscar((int)dr["clienteId"]);
                    objAgendamento.objUsuario = usuarioDAO.Buscar((int)dr["usuarioId"]);
                    objAgendamento.AgendamentoId = Convert.ToInt32(dr["agendamentoId"]);

                    listaAgendamento.Add(objAgendamento);
                }
            }
            
            dr.Close();

            return listaAgendamento;
        }
        #endregion
        #region Buscar
        public Agendamento Buscar(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            
            Agendamento objAgendamento = new Agendamento();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Agendamentos where agendamentoId = @agendamentoId";
            comando.Parameters.AddWithValue("@agendamentoId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objAgendamento.HorarioAgendamento = Convert.ToDateTime(dr["horarioAgendamento"]);
                objAgendamento.Indicacao = Convert.ToBoolean(dr["indicacao"]);
                objAgendamento.Status = (Status)Enum.Parse(typeof(Status), dr["enumStatus"].ToString());
                objAgendamento.objCliente = clienteDAO.Buscar((int)dr["clienteId"]);
                objAgendamento.objUsuario = usuarioDAO.Buscar((int)dr["usuarioId"]);
            }
            else
            {
                objAgendamento = null;
            }
            dr.Close();
            return objAgendamento;

        #endregion

        }
    }
}