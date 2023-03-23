using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sis_RosangelaCunha.Models
{
    public class AquisicaoContrato
    {
        #region Atributos
        public int AquisicaoId { get; set; }
        public DateTime DataAquisicao { get; set; }
        public string Desconto { get; set; }
        public string Migracoes { get; set; }
        public string Observacoes { get; set; }
        public string LoginSerasa1 { get; set; }
        public string LoginSerasa2 { get; set; }
        public string LoginSerasa3 { get; set; }
        public string LoginSerasa4 { get; set; }
        public string LoginSerasa5 { get; set; }

        #endregion
        public ICollection<ItemAnexado> listaItemAnexado { get; set; }

        public Cliente objCliente { get; set; }
        public Usuario objUsuario { get; set; }
        public Agendamento objAgendamento { get; set; }
        public Plano objPlano { get; set; }
        public AvaliacaoContrato objAvaliacao { get; set; }
        
        public AquisicaoContrato()
        {
            //listaItemAnexado = new List<ItemAnexado>();
            objUsuario = new Usuario();
            objAgendamento = new Agendamento();
            objPlano = new Plano();
            objAvaliacao = new AvaliacaoContrato();
            objCliente = new Cliente();
            
        }
    }
}
