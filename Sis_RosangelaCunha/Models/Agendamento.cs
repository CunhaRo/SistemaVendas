using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
//using Google.Apis.Calendar.v3.data;

namespace Sis_RosangelaCunha.Models
{
    public class Agendamento
    {
        #region Atributos
        public int AgendamentoId { get; set; }
        public DateTime HorarioAgendamento { get; set; }
        public bool Indicacao { get; set; }
        public string IndicadoPor { get; set; }
        public Status Status { get; set; }
        #endregion
        public Usuario objUsuario { get; set; }
        public Cliente objCliente { get; set; }
        public Agendamento()
        {
            objUsuario = new Usuario();
            objCliente = new Cliente();
        }
    }
}