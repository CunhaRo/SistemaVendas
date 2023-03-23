using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sis_RosangelaCunha.Models;
using System.Web.Mvc;
using Sis_RosangelaCunha.DAO;
using System.ComponentModel.DataAnnotations;

namespace Sis_RosangelaCunha.ViewModel
{
    public class AgendamentoViewModel
    {
        #region Atributos e Propriedades
        public int AgendamentoId { get; set; }
        public DateTime HorarioAgendamento { get; set; }
        public bool Indicacao { get; set; }
        public string IndicadoPor { get; set; }
        public Status Status { get; set; }
        #endregion

        public int IdCliente { get; set; }
        public List<SelectListItem> Clientes{get; set;}
        public int IdUsuario { get; set; }
        public List<SelectListItem> Usuarios{get; set;}
 

        public ClienteDAO clienteDAO = new ClienteDAO();
        public UsuarioDAO usuarioDAO = new UsuarioDAO();

        public AgendamentoViewModel()
        {
            Clientes = CarregaClientes();
            HorarioAgendamento = DateTime.Now;
        }

        public List<SelectListItem> CarregaClientes()         
        {             
            var lista = new List<SelectListItem>();              
            var clientes = clienteDAO.BuscarTodos();                          
            foreach (var item in clientes)             
            {                 
                var option = new SelectListItem() 
                {                     
                    Text = item.NomeFantasia                    
                    , Value = item.ClienteId.ToString()
                };                  
                lista.Add(option);
            } 

             
            return lista;         
        }

        public List<SelectListItem> CarregaUsuarios()         
        {             
            var lista = new List<SelectListItem>();              
            var usuarios = usuarioDAO.BuscarTodos();                          
            foreach (var item in usuarios)             
            {                 
                var option = new SelectListItem() 
                {                     
                    Text = item.Nome                    
                    , Value = item.UsuarioId.ToString()
                };                  
                lista.Add(option);
            } 

             
            return lista;         
        }


        public List<SelectListItem> CarregaAgendamento()
        {
            var lista = new List<SelectListItem>();

            var agendamentos = new AgendamentoDAO().BuscarTodos();

            foreach (var item in agendamentos)
            {
                var option = new SelectListItem()
                {
                    Text = string.Format("{0} - {1}", item.AgendamentoId, item.HorarioAgendamento.ToShortDateString())
                    , Value = item.AgendamentoId.ToString()
                };
                lista.Add(option);
            }


            return lista;
        }

    }
}