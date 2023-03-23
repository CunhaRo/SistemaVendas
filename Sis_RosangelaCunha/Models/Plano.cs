using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Sis_RosangelaCunha.DAO;

namespace Sis_RosangelaCunha.Models
{
    public class Plano
    {
        public int PlanoId { get; set; }
        public string NomePlano { get; set; }
        public string Valor { get; set; }
        public enumMensalidades Mensalidades { get; set; }
        public DescricaoPlano ObjDescricaoPlano { get; set; }

        public Plano()
        {
            ObjDescricaoPlano = new DescricaoPlano();
            
        }
      
    }
}