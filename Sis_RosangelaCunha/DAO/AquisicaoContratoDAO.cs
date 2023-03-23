using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sis_RosangelaCunha.DAO
{
    public class AquisicaoContratoDAO
    {
        #region Gravar
        public void Gravar(AquisicaoContrato aquisicao)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into AquisicoesContratos(DataAquisicao,Desconto,Migracoes,Observacoes,LoginSerasa1,LoginSerasa2,LoginSerasa3,LoginSerasa4,LoginSerasa5,UsuarioId,AgendamentoId,AvaliacaoId,ClienteId,PlanoId) values (@dataAquisicao,@desconto,@migracoes,@observacoes,@loginSerasa1,@loginSerasa2,@loginSerasa3,@loginSerasa4,@loginSerasa5,@usuarioId,@agendamentoId,@avaliacaoId,@clienteId,@planoId)";
            comando.Parameters.AddWithValue("@dataAquisicao", aquisicao.DataAquisicao);
            comando.Parameters.AddWithValue("@desconto", aquisicao.Desconto);
            comando.Parameters.AddWithValue("@migracoes", aquisicao.Migracoes);
            comando.Parameters.AddWithValue("@observacoes", aquisicao.Observacoes);
            comando.Parameters.AddWithValue("@loginSerasa1", aquisicao.LoginSerasa1);
            comando.Parameters.AddWithValue("@loginSerasa2", aquisicao.LoginSerasa2);
            comando.Parameters.AddWithValue("@loginSerasa3", aquisicao.LoginSerasa3);
            comando.Parameters.AddWithValue("@loginSerasa4", aquisicao.LoginSerasa4);
            comando.Parameters.AddWithValue("@loginSerasa5", aquisicao.LoginSerasa5);
            comando.Parameters.AddWithValue("@usuarioId", aquisicao.objUsuario.UsuarioId);
            comando.Parameters.AddWithValue("@agendamentoId", aquisicao.objAgendamento.AgendamentoId);
            comando.Parameters.AddWithValue("@avaliacaoId", aquisicao.objAvaliacao.AvaliacaoId);
            comando.Parameters.AddWithValue("@clienteId", aquisicao.objCliente.ClienteId);
            comando.Parameters.AddWithValue("@planoId", aquisicao.objPlano.PlanoId);
            
            //comando.Parameters.AddWithValue("@requerimentoId", aquisicao.listaItemAnexado);

            ConexaoBanco.CRUD(comando);

            ItemAnexadoDAO anexo = new ItemAnexadoDAO();

            foreach (var item in aquisicao.listaItemAnexado)
            {
                anexo.Inserir(item);
            }          
        }
        #endregion
        #region Alterar
        public void Alterar(AquisicaoContrato aquisicao)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update AquisicoesContratos set DataAquisicao=@dataAquisicao,Desconto=@desconto,Migracoes=@migracoes,Observacoes=@observacoes,LoginSerasa1=@loginSerasa1,LoginSerasa2=@loginSerasa2,LoginSerasa3=@loginSerasa3,LoginSerasa4=@loginSerasa4,LoginSerasa5=@loginSerasa5,UsuarioId=@usuarioId,AgendamentoId=@agendamentoId,AvaliacaoId=@avaliacaoId,ClienteId=@clienteId,PlanoId=@planoId where AquisicaoId=@aquisicaoId";
            comando.Parameters.AddWithValue("@dataAquisicao", aquisicao.DataAquisicao);
            comando.Parameters.AddWithValue("@desconto", aquisicao.Desconto);
            comando.Parameters.AddWithValue("@migracoes", aquisicao.Migracoes);
            comando.Parameters.AddWithValue("@observacoes", aquisicao.Observacoes);
            comando.Parameters.AddWithValue("@loginSerasa1", aquisicao.LoginSerasa1);
            comando.Parameters.AddWithValue("@loginSerasa2", aquisicao.LoginSerasa2);
            comando.Parameters.AddWithValue("@loginSerasa3", aquisicao.LoginSerasa3);
            comando.Parameters.AddWithValue("@loginSerasa4", aquisicao.LoginSerasa4);
            comando.Parameters.AddWithValue("@loginSerasa5", aquisicao.LoginSerasa5);
            comando.Parameters.AddWithValue("@usuarioId", aquisicao.objUsuario.UsuarioId);
            comando.Parameters.AddWithValue("@agendamentoId", aquisicao.objAgendamento.AgendamentoId);
            comando.Parameters.AddWithValue("@avaliacaoId", aquisicao.objAvaliacao.AvaliacaoId);
            comando.Parameters.AddWithValue("@clienteId", aquisicao.objCliente.ClienteId);
            comando.Parameters.AddWithValue("@planoId", aquisicao.objPlano.PlanoId);
            comando.Parameters.AddWithValue("@aquisicaoId", aquisicao.AquisicaoId);
            //comando.Parameters.AddWithValue("@requerimentoId", aquisicao.listaItemAnexado);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(AquisicaoContrato aquisicao)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from AquisicoesContratos where AquisicaoId=@aquisicaoId";
            comando.Parameters.AddWithValue("@aquisicaoId", aquisicao.AquisicaoId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<AquisicaoContrato> BuscarTodos()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            PlanoDAO planoDAO = new PlanoDAO();
            AvaliacaoContratoDAO avaliacaoDAO = new AvaliacaoContratoDAO();
            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            IList<AquisicaoContrato> listaAquisicao = new List<AquisicaoContrato>();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From AquisicoesContratos";
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    AquisicaoContrato objAquisicao = new AquisicaoContrato();

                    objAquisicao.AquisicaoId = Convert.ToInt32(dr["AquisicaoId"]);
                    objAquisicao.DataAquisicao = Convert.ToDateTime(dr["DataAquisicao"]);
                    objAquisicao.Desconto = dr["Desconto"].ToString();
                    objAquisicao.Migracoes = dr["Migracoes"].ToString();
                    objAquisicao.Observacoes = dr["Observacoes"].ToString();
                    objAquisicao.LoginSerasa1 = dr["LoginSerasa1"].ToString();
                    objAquisicao.LoginSerasa2 = dr["LoginSerasa2"].ToString();
                    objAquisicao.LoginSerasa3 = dr["LoginSerasa3"].ToString();
                    objAquisicao.LoginSerasa4 = dr["LoginSerasa4"].ToString();
                    objAquisicao.LoginSerasa5 = dr["LoginSerasa5"].ToString();
                    objAquisicao.objUsuario = usuarioDAO.Buscar((int)dr["UsuarioId"]);
                    objAquisicao.objAgendamento = agendamentoDAO.Buscar((int)dr["AgendamentoId"]);
                    objAquisicao.objAvaliacao = avaliacaoDAO.Buscar((int)dr["AvaliacaoId"]);
                    objAquisicao.objCliente = clienteDAO.Buscar((int)dr["clienteId"]);
                    objAquisicao.objPlano = planoDAO.Buscar((int)dr["planoId"]);


                    listaAquisicao.Add(objAquisicao);
                }
            }
            else
            {
                listaAquisicao = null;
            }
            dr.Close();
            return listaAquisicao;
        }
        #endregion
        #region Buscar
        public AquisicaoContrato Buscar(int id)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            PlanoDAO planoDAO = new PlanoDAO();
            AvaliacaoContratoDAO avaliacaoDAO = new AvaliacaoContratoDAO();
            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            AquisicaoContrato objAquisicao = new AquisicaoContrato();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From AquisicoesContratos where AquisicaoId = @aquisicaoId";
            comando.Parameters.AddWithValue("@aquisicaiId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objAquisicao.AquisicaoId = Convert.ToInt32(dr["AquisicaoId"]);
                objAquisicao.DataAquisicao = Convert.ToDateTime(dr["DataAquisicao"]);
                objAquisicao.Desconto = dr["Desconto"].ToString();
                objAquisicao.Migracoes = dr["Migracoes"].ToString();
                objAquisicao.Observacoes = dr["Observacoes"].ToString();
                objAquisicao.LoginSerasa1 = dr["LoginSerasa1"].ToString();
                objAquisicao.LoginSerasa2 = dr["LoginSerasa2"].ToString();
                objAquisicao.LoginSerasa3 = dr["LoginSerasa3"].ToString();
                objAquisicao.LoginSerasa4 = dr["LoginSerasa4"].ToString();
                objAquisicao.LoginSerasa5 = dr["LoginSerasa5"].ToString();
                objAquisicao.objUsuario = usuarioDAO.Buscar((int)dr["UsuarioId"]);
                objAquisicao.objAgendamento = agendamentoDAO.Buscar((int)dr["AgendamentoId"]);
                objAquisicao.objAvaliacao = avaliacaoDAO.Buscar((int)dr["AvaliacaoId"]);
                objAquisicao.objCliente = clienteDAO.Buscar((int)dr["clienteId"]);
                objAquisicao.objPlano = planoDAO.Buscar((int)dr["planoId"]);
            }
            else
            {
                objAquisicao = null;
            }
            dr.Close();
            return objAquisicao;
        }
        #endregion


        public IList<AquisicaoContrato> BuscarporTermo(string busca)
        {
            IList<AquisicaoContrato> listaAquisicao = new List<AquisicaoContrato>();
            ClienteDAO clienteDAO = new ClienteDAO();
            PlanoDAO planoDAO = new PlanoDAO();
            AvaliacaoContratoDAO avaliacaoDAO = new AvaliacaoContratoDAO();
            AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
            UsuarioDAO usuarioDAO = new UsuarioDAO();

            AquisicaoContrato objAquisicao = new AquisicaoContrato();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;

            var builder = new StringBuilder();

            builder.Append("Select a.* From AquisicoesContratos a");
            

            if (!string.IsNullOrEmpty(busca))
            {
                builder.Append(" inner join Clientes c on a.clienteId = c.clienteId ");
                builder.AppendFormat(" where  c.Nome like '{0}'", busca);
            }

            comando.CommandText = builder.ToString();
                


            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objAquisicao.AquisicaoId = Convert.ToInt32(dr["AquisicaoId"]);
                objAquisicao.DataAquisicao = Convert.ToDateTime(dr["DataAquisicao"]);
                objAquisicao.Desconto = dr["Desconto"].ToString();
                objAquisicao.Migracoes = dr["Migracoes"].ToString();
                objAquisicao.Observacoes = dr["Observacoes"].ToString();
                objAquisicao.LoginSerasa1 = dr["LoginSerasa1"].ToString();
                objAquisicao.LoginSerasa2 = dr["LoginSerasa2"].ToString();
                objAquisicao.LoginSerasa3 = dr["LoginSerasa3"].ToString();
                objAquisicao.LoginSerasa4 = dr["LoginSerasa4"].ToString();
                objAquisicao.LoginSerasa5 = dr["LoginSerasa5"].ToString();
                objAquisicao.objUsuario = usuarioDAO.Buscar((int)dr["UsuarioId"]);
                //objAquisicao.objAgendamento = agendamentoDAO.Buscar((int)dr["AgendamentoId"]);
                //objAquisicao.objAvaliacao = avaliacaoDAO.Buscar((int)dr["AvaliacaoId"]);
                objAquisicao.objCliente = clienteDAO.Buscar((int)dr["clienteId"]);
                objAquisicao.objPlano = planoDAO.Buscar((int)dr["planoId"]);


                listaAquisicao.Add(objAquisicao);
            }

            dr.Close();

            return listaAquisicao;
       }
    }
}