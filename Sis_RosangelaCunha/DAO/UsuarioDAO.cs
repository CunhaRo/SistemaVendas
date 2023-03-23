using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Data;
using System.Data.SqlClient;

namespace Sis_RosangelaCunha.DAO
{
    public class UsuarioDAO
    {
        #region Gravar
        public void Gravar(Usuario usuario)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Insert into Usuarios (Login,Senha,Nome,Email,Celular,EnumPerfilAcesso) values (@login,@senha,@nome,@email,@celular,@enumPerfilAcesso)";
            comando.Parameters.AddWithValue("@login", usuario.Login);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@celular", usuario.Celular);
            comando.Parameters.AddWithValue("@enumPerfilAcesso", usuario.EnumPerfilAcesso);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Alterar
        public void Editar(Usuario usuario)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Update Usuarios set Login=@login,Senha=@senha,Nome=@nome,Email=@email,Celular=@celular,EnumPerfilAcesso=@enumPerfilAcesso where UsuarioId=@usuarioId";
            comando.Parameters.AddWithValue("@login", usuario.Login);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@celular", usuario.Celular);
            comando.Parameters.AddWithValue("@enumPerfilAcesso", usuario.EnumPerfilAcesso);
            comando.Parameters.AddWithValue("@usuarioId", usuario.UsuarioId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region Excluir
        public void Excluir(Usuario usuario)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Delete from Usuarios where UsuarioId=@usuarioId";
            comando.Parameters.AddWithValue("@usuarioId", usuario.UsuarioId);

            ConexaoBanco.CRUD(comando);
        }
        #endregion
        #region BuscarTodos
        public IList<Usuario> BuscarTodos()
        {
            IList<Usuario> listaUsuario = new List<Usuario>();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Usuarios";
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Usuario objUsuario = new Usuario();

                    objUsuario.UsuarioId = Convert.ToInt32(dr["UsuarioId"]);
                    objUsuario.Login = dr["Login"].ToString();
                    objUsuario.Senha = dr["Senha"].ToString();
                    objUsuario.Nome = dr["Nome"].ToString();
                    objUsuario.Email = dr["Email"].ToString();
                    objUsuario.Celular = dr["Celular"].ToString();    
                    objUsuario.EnumPerfilAcesso= (enumPerfilAcesso)Enum.Parse(typeof(enumPerfilAcesso), dr["EnumPerfilAcesso"].ToString());
                    listaUsuario.Add(objUsuario);
                }
            }
            
            dr.Close();
            return listaUsuario;
        }
        #endregion

        #region Buscar
        public Usuario Buscar(int id)
        {
            Usuario objUsuario = new Usuario();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Usuarios (nolock)where UsuarioId = @usuarioId";
            comando.Parameters.AddWithValue("@usuarioId", id);

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();

                objUsuario.UsuarioId = Convert.ToInt32(dr["UsuarioId"]);
                objUsuario.Login = dr["Login"].ToString();
                objUsuario.Senha = dr["Senha"].ToString();
                objUsuario.Nome = dr["Nome"].ToString();
                objUsuario.Email = dr["Email"].ToString();
                objUsuario.Celular = dr["Celular"].ToString();
                objUsuario.EnumPerfilAcesso = (enumPerfilAcesso)Enum.Parse(typeof(enumPerfilAcesso), dr["EnumPerfilAcesso"].ToString());
            }
            else
            {
                objUsuario = null;
            }
            dr.Close();
            return objUsuario;

        #endregion
        }
        public IList<Usuario> BuscarNome(string nome)
        {
            IList<Usuario> listaUsuarios = new List<Usuario>();

            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM Usuarios Where Nome LIKE @nome";

            comando.Parameters.AddWithValue("@nome", string.Format("{0}%", nome));

            SqlDataReader dr = ConexaoBanco.Selecionar(comando);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Usuario objUsuario = new Usuario();

                    objUsuario.UsuarioId = Convert.ToInt32(dr["UsuarioId"]);
                    objUsuario.Login = dr["Login"].ToString();
                    objUsuario.Senha = dr["Senha"].ToString();
                    objUsuario.Nome = dr["Nome"].ToString();
                    objUsuario.Email = dr["Email"].ToString();
                    objUsuario.Celular = dr["Celular"].ToString();
                    objUsuario.EnumPerfilAcesso = (enumPerfilAcesso)Enum.Parse(typeof(enumPerfilAcesso), dr["EnumPerfilAcesso"].ToString());

                    listaUsuarios.Add(objUsuario);
                }
            }
            else
            {
                listaUsuarios = null;
            }
            return listaUsuarios;
        }
        #region BuscarUsuario
        public Usuario BuscarUsuario(Usuario usuario)
        {
            Usuario objUsuario = new Usuario();
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "Select * From Usuarios (nolock)where Login=@login and Senha=@senha";
            comando.Parameters.AddWithValue("@login", usuario.Login);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            SqlDataReader dr = ConexaoBanco.Selecionar(comando);
            if (dr.HasRows)
            {
                dr.Read();
                objUsuario.UsuarioId = Convert.ToInt32(dr["UsuarioId"]);
                objUsuario.Login = dr["Login"].ToString();
                objUsuario.Senha = dr["Senha"].ToString();
                objUsuario.Nome = dr["Nome"].ToString();
                objUsuario.Email = dr["Email"].ToString();
                objUsuario.Celular = dr["Celular"].ToString();
                objUsuario.EnumPerfilAcesso = (enumPerfilAcesso)Enum.Parse(typeof(enumPerfilAcesso), dr["EnumPerfilAcesso"].ToString());
            }
            else
            {
                objUsuario = null;
            }
            dr.Close();
            return objUsuario;

        #endregion
        }


        internal void AlterarSenha(Usuario user)
        {
            throw new NotImplementedException();
        }

        internal IList<Usuario> BuscarPorLogin(string login)
        {
            throw new NotImplementedException();
        }
    }
}