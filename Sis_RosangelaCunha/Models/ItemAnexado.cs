using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sis_RosangelaCunha.Models
{
    public class ItemAnexado
    {
        public int ItensId { get; set; }

        public string Nome { get; set; }
        
        public string MimeType { get; set; }
        
        public AquisicaoContrato objAquisicao { get; set; }
        
        public virtual byte[] Arquivo { get; set; }

        //public virtual string Nome { get; set; }

        //public virtual string Local { get; set; }

        //public virtual string MimeType { get; set; }

        //public virtual string RowGuid { get; set; }



        public ItemAnexado()
        {
            objAquisicao = new AquisicaoContrato();
        }
    }
}