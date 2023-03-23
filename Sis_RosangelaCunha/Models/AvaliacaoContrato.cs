using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sis_RosangelaCunha.Models
{
    public class AvaliacaoContrato
    {
        public int AvaliacaoId { get; set; }
        public string Contato { get; set; }
        public DateTime DataContato { get; set; }
        public string Comentarios { get; set; }
        public bool Features { get; set; }
        public bool Treinamento { get; set; }
        public string Atendimento { get; set; }
    }
}