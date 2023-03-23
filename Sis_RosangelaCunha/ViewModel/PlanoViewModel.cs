using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.DAO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Sis_RosangelaCunha.Models;

namespace Sis_RosangelaCunha.ViewModel
{
    public class PlanoViewModel
    {
        private readonly  PlanoDAO planoDao = new PlanoDAO();

        #region Atributos
        public int PlanoId { get; set; }
        public string NomePlano { get; set; }
        public string Valor { get; set; }
        public enumMensalidades Mensalidades { get; set; }
        #endregion


        #region Teste 

        public ICollection<DescricaoPlano> listaDescricaoPlanos { get; set; }

        public int IdDescricao { get; set; }

        public List<SelectListItem> Descricoes { get; set;}

        public DescricaoPlanoDAO descricaoPlanoDAO = new DescricaoPlanoDAO();

        public List<SelectListItem> CarregarDescricao()
        {
            var lista = new List<SelectListItem>();
            var descricoes = descricaoPlanoDAO.BuscarTodos();
            foreach (var item in descricoes)
            {
                var option = new SelectListItem()
                {
                    Text = item.Descricao
                    ,
                    Value = item.DescricaoId.ToString()
                };
                lista.Add(option);
            }


            return lista;
        }

        public PlanoViewModel()
        {
            Descricoes = CarregarDescricao();
        }

        public List<PlanoViewModel> BuscaViewModelporBusca()
        {
            var lista = new List<PlanoViewModel>();

            var planos = planoDao.BuscarTodos();

            foreach (var item in planos)
            {
                lista.Add(new PlanoViewModel()
                {
                    
                });
            }
            return lista;
        }
        #endregion
    }
}