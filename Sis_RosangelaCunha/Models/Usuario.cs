using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sis_RosangelaCunha.Models
{
    public class Usuario
    {
        #region Atributos
        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public enumPerfilAcesso EnumPerfilAcesso { get; set; }

        private string confirmeSenha;

        //[Required(ErrorMessage = "Campo obrigatório")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Senha Antiga")]
        private string senhaAntiga;
        #endregion


        #region OUTROS


        public string ConfirmeSenha
        {
            get { return confirmeSenha; }
            set { confirmeSenha = value; }
        }
        public string SenhaAntiga
        {
            get { return senhaAntiga; }
            set { senhaAntiga = value; }
        }
        #endregion
    }
}