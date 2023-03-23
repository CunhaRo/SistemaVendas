using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;

namespace Sis_RosangelaCunha.DAO
{
    public class ClienteDAO
    {
        #region Gravar
        public void Gravar(Cliente cliente)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into Clientes(NomeFantasia,RazaoSocial,Cnpj,Ie,ResponsavelEmpresa,CpfResponsavel,Cidade,EnumEstado,Cep,Endereco,Bairro,Complemento,Email,Telefone1,Telefone2,Celular) values (@nomeFantasia,@razaoSocial,@cnpj,@ie,@responsavelEmpresa,@cpfResponsavel,@cidade,@enumEstado,@cep,@endereco,@bairro,@complemento,@email,@telefone1,@telefone2,@celular)";
            comando.Parameters.AddWithValue("@nomeFantasia", cliente.NomeFantasia);
            comando.Parameters.AddWithValue("@razaoSocial", cliente.RazaoSocial);
            comando.Parameters.AddWithValue("@cnpj", cliente.Cnpj);
            comando.Parameters.AddWithValue("@ie", cliente.Ie);
            comando.Parameters.AddWithValue("@responsavelEmpresa", cliente.ResponsavelEmpresa);
            comando.Parameters.AddWithValue("@cpfResponsavel", cliente.CpfResponsavel);
            comando.Parameters.AddWithValue("@cidade", cliente.Cidade);
            comando.Parameters.AddWithValue("@enumEstado", cliente.EnumEstado);
            comando.Parameters.AddWithValue("@cep", cliente.Cep);
            comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
            comando.Parameters.AddWithValue("@bairro", cliente.Bairro);
            comando.Parameters.AddWithValue("@complemento", cliente.Complemento);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@telefone1", cliente.Telefone1);
            comando.Parameters.AddWithValue("@telefone2", cliente.Telefone2);
            comando.Parameters.AddWithValue("@celular", cliente.Celular);
            
            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Editar(Cliente cliente)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update Clientes set NomeFantasia=@nomeFantasia,RazaoSocial=@razaoSocial,Cnpj=@cnpj,Ie=@ie,ResponsavelEmpresa=@responsavelEmpresa,CpfResponsavel=@cpfResponsavel,Cidade=@cidade,EnumEstado=@enumEstado,Cep=@cep,Endereco=@endereco,Bairro=@bairro,Complemento=@complemento,Email=@email,Telefone1=@Telefone1,Telefone2=@telefone2,Celular=@celular where ClienteId=@clienteId";
            comando.Parameters.AddWithValue("@nomeFantasia", cliente.NomeFantasia);
            comando.Parameters.AddWithValue("@razaoSocial", cliente.RazaoSocial);
            comando.Parameters.AddWithValue("@cnpj", cliente.Cnpj);
            comando.Parameters.AddWithValue("@ie", cliente.Ie);
            comando.Parameters.AddWithValue("@responsavelEmpresa", cliente.ResponsavelEmpresa);
            comando.Parameters.AddWithValue("@cpfResponsavel", cliente.CpfResponsavel);
            comando.Parameters.AddWithValue("@cidade", cliente.Cidade);
            comando.Parameters.AddWithValue("@enumEstado", cliente.EnumEstado);
            comando.Parameters.AddWithValue("@cep", cliente.Cep);
            comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
            comando.Parameters.AddWithValue("@bairro", cliente.Bairro);
            comando.Parameters.AddWithValue("@complemento", cliente.Complemento);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@telefone1", cliente.Telefone1);
            comando.Parameters.AddWithValue("@telefone2", cliente.Telefone2);
            comando.Parameters.AddWithValue("@celular", cliente.Celular);
            comando.Parameters.AddWithValue("@clienteId", cliente.ClienteId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(Cliente cliente)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from Clientes where ClienteId=@clienteId";
            comando.Parameters.AddWithValue("@clienteId", cliente.ClienteId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<Cliente> BuscarTodos()
        {
            IList<Cliente> listaClientes = new List<Cliente>();

            SqlCommand comando = new SqlCommand();

            comando.CommandType = CommandType.Text;

            comando.CommandText = "Select * From Clientes";

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Cliente objCliente = new Cliente();

                    objCliente.ClienteId = Convert.ToInt32(dr["ClienteId"]);
                    objCliente.NomeFantasia = dr["NomeFantasia"].ToString();
                    objCliente.RazaoSocial = dr["RazaoSocial"].ToString();
                    objCliente.Cnpj = dr["Cnpj"].ToString();
                    objCliente.Ie = dr["Ie"].ToString();
                    objCliente.ResponsavelEmpresa = dr["ResponsavelEmpresa"].ToString();
                    objCliente.CpfResponsavel = dr["CpfResponsavel"].ToString();
                    objCliente.Cidade = dr["Cidade"].ToString();
                    objCliente.EnumEstado = (enumEstado)Enum.Parse(typeof(enumEstado), dr["EnumEstado"].ToString());
                    objCliente.Cep = dr["Cep"].ToString();
                    objCliente.Endereco = dr["Endereco"].ToString();
                    objCliente.Bairro = dr["Bairro"].ToString();
                    objCliente.Complemento = dr["Complemento"].ToString();
                    objCliente.Email = dr["Email"].ToString();
                    objCliente.Telefone1 = dr["Telefone1"].ToString();
                    objCliente.Telefone2 = dr["Telefone2"].ToString();
                    objCliente.Celular = dr["Celular"].ToString();
                    
                    
                    listaClientes.Add(objCliente);
                }
            }
            
            dr.Close();

            return listaClientes;
        }
        #endregion
        #region Buscar
        public Cliente Buscar(int id)
        {
            Cliente objCliente = new Cliente();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Clientes where ClienteId = @clienteId";
            comando.Parameters.AddWithValue("@clienteId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objCliente.ClienteId = Convert.ToInt32(dr["ClienteId"]);
                objCliente.NomeFantasia = dr["NomeFantasia"].ToString();
                objCliente.RazaoSocial = dr["RazaoSocial"].ToString();
                objCliente.Cnpj = dr["Cnpj"].ToString();
                objCliente.Ie = dr["Ie"].ToString();
                objCliente.ResponsavelEmpresa = dr["ResponsavelEmpresa"].ToString();
                objCliente.CpfResponsavel = dr["CpfResponsavel"].ToString();
                objCliente.Cidade = dr["Cidade"].ToString();
                objCliente.EnumEstado = (enumEstado)Enum.Parse(typeof(enumEstado), dr["EnumEstado"].ToString());
                objCliente.Cep = dr["Cep"].ToString();
                objCliente.Endereco = dr["Endereco"].ToString();
                objCliente.Bairro = dr["Bairro"].ToString();
                objCliente.Complemento = dr["Complemento"].ToString();
                objCliente.Email = dr["Email"].ToString();
                objCliente.Telefone1 = dr["Telefone1"].ToString();
                objCliente.Telefone2 = dr["Telefone2"].ToString();
                objCliente.Celular = dr["Celular"].ToString();
            }
            else
            {
                objCliente = null;
            }
            dr.Close();
            return objCliente;

        #endregion
        }
        #region BuscarFantasia
        public IList<Cliente> BuscarFantasia(string fantasia)
        {
            IList<Cliente> listaCliente = new List<Cliente>();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM Clientes Where NomeFantasia LIKE @nomeFantasia";

            comando.Parameters.AddWithValue("@nomeFantasia", string.Format("{0}%", fantasia));

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Cliente objCliente = new Cliente();

                    objCliente.ClienteId = Convert.ToInt32(dr["ClienteId"]);
                    objCliente.NomeFantasia = dr["NomeFantasia"].ToString();
                    objCliente.RazaoSocial = dr["RazaoSocial"].ToString();
                    objCliente.Cnpj = dr["Cnpj"].ToString();
                    objCliente.Ie = dr["Ie"].ToString();
                    objCliente.ResponsavelEmpresa = dr["ResponsavelEmpresa"].ToString();
                    objCliente.CpfResponsavel = dr["CpfResponsavel"].ToString();
                    objCliente.Cidade = dr["Cidade"].ToString();
                    objCliente.EnumEstado = (enumEstado)Enum.Parse(typeof(enumEstado), dr["EnumEstado"].ToString());
                    objCliente.Cep = dr["Cep"].ToString();
                    objCliente.Endereco = dr["Endereco"].ToString();
                    objCliente.Bairro = dr["Bairro"].ToString();
                    objCliente.Complemento = dr["Complemento"].ToString();
                    objCliente.Email = dr["Email"].ToString();
                    objCliente.Telefone1 = dr["Telefone1"].ToString();
                    objCliente.Telefone2 = dr["Telefone2"].ToString();
                    objCliente.Celular = dr["Celular"].ToString();

                    listaCliente.Add(objCliente);
                }
            }
            else
            {
                listaCliente = null;
            }
            return listaCliente;
        }
        #endregion
        #region BuscarRazaoSocial
        public IList<Cliente> BuscarPorRazao(string razao)
        {
            IList<Cliente> listaCliente = new List<Cliente>();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM Clientes Where RazaoSocial LIKE @razaoSocial";

            comando.Parameters.AddWithValue("@razaoSocial", string.Format("%{0}%", razao));

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Cliente objCliente = new Cliente();
                    objCliente.ClienteId = Convert.ToInt32(dr["ClienteId"]);
                    objCliente.NomeFantasia = dr["NomeFantasia"].ToString();
                    objCliente.RazaoSocial = dr["RazaoSocial"].ToString();
                    objCliente.Cnpj = dr["Cnpj"].ToString();
                    objCliente.Ie = dr["Ie"].ToString();
                    objCliente.ResponsavelEmpresa = dr["ResponsavelEmpresa"].ToString();
                    objCliente.CpfResponsavel = dr["CpfResponsavel"].ToString();
                    objCliente.Cidade = dr["Cidade"].ToString();
                    objCliente.EnumEstado = (enumEstado)Enum.Parse(typeof(enumEstado), dr["EnumEstado"].ToString());
                    objCliente.Cep = dr["Cep"].ToString();
                    objCliente.Endereco = dr["Endereco"].ToString();
                    objCliente.Bairro = dr["Bairro"].ToString();
                    objCliente.Complemento = dr["Complemento"].ToString();
                    objCliente.Email = dr["Email"].ToString();
                    objCliente.Telefone1 = dr["Telefone1"].ToString();
                    objCliente.Telefone2 = dr["Telefone2"].ToString();
                    objCliente.Celular = dr["Celular"].ToString();

                    listaCliente.Add(objCliente);
                }
            }
            else
            {
                listaCliente = null;
            }
            return listaCliente;
        }
        #endregion
    }
}