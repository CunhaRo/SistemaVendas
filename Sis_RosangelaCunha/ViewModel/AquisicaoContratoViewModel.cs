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
    public class AquisicaoContratoViewModel
    {

        #region Variáveis

        private readonly AquisicaoContratoDAO aquisicaoDAO = new AquisicaoContratoDAO();

        #endregion


        public AquisicaoContratoViewModel(int idAgendamento, bool semagendamento = false)
        {
            var agendamento = new Agendamento();

            if (idAgendamento > 0 && !semagendamento)
            {
                agendamento = agendamentoDAO.Buscar(idAgendamento);
                Clientes = CarregaClientes(agendamento.objCliente.ClienteId);
                if (Clientes != null) IdCliente = agendamento.objCliente.ClienteId;
            }
            else {
                Clientes = CarregaClientes();
            }
            
            Agendamentos = CarregaAgendamentos();
            Planos = CarregaPlanos();
            Avaliacoes = CarregaAvaliacoes();
            DataAquisicao = DateTime.Now;
        }



        public AquisicaoContratoViewModel()
        {
            Clientes = CarregaClientes();

            Planos = CarregaPlanos();

            Usuarios = CarregaUsuarios();

            Agendamentos = CarregaAgendamentos();

            Avaliacoes = CarregaAvaliacoes();

            DataAquisicao = DateTime.Now;
        }


        #region Atributos e Propriedades

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

        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdAgendamento { get; set; }
        public int IdPlano { get; set; }
        public int IdAvaliacao { get; set; }

        public ClienteDAO clienteDAO = new ClienteDAO();
        public UsuarioDAO usuarioDAO = new UsuarioDAO();
        public AgendamentoDAO agendamentoDAO = new AgendamentoDAO();
        public PlanoDAO planoDAO = new PlanoDAO();


        public Agendamento agendamento = new Agendamento();

        public List<SelectListItem> Avaliacoes { get; set; }
        public List<SelectListItem> Planos { get; set; }
        public List<SelectListItem> Agendamentos { get; set; }
        public List<SelectListItem> Usuarios { get; set; }
        public List<SelectListItem> Clientes { get; set; }

        public ICollection<ItemAnexado> listaItemAnexado { get; set; }
        

        /// <summary>
        /// Carrega Clientes
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> CarregaClientes(int id = 0)
        {
            var lista = new List<SelectListItem>();
            var clientes = clienteDAO.BuscarTodos();
            foreach (var item in clientes)
            {
                var option = new SelectListItem()
                {
                    Text = item.NomeFantasia
                    , Value = item.ClienteId.ToString()
                    , Selected = (item.ClienteId == id)
                };
                lista.Add(option);
            }


            return lista;
        }

        /// <summary>
        /// Carrega Usuários
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> CarregaUsuarios()
        {
            var lista = new List<SelectListItem>();
            var usuarios = usuarioDAO.BuscarTodos();
            foreach (var item in usuarios)
            {
                var option = new SelectListItem()
                {
                    Text = item.Nome
                    ,
                    Value = item.UsuarioId.ToString()
                };
                lista.Add(option);
            }


            return lista;
        }


        /// <summary>
        /// Carrega Agendamento
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> CarregaAgendamentos()
        {
            var lista = new List<SelectListItem>();
            var agendamentos = agendamentoDAO.BuscarTodos();
            foreach (var item in agendamentos)
            {
                var option = new SelectListItem()
                {
                    Text = item.HorarioAgendamento.ToShortDateString()
                    ,
                    Value = item.AgendamentoId.ToString()
                };
                lista.Add(option);
            }

            return lista;
        }

        /// <summary>
        /// Carrega Planos
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> CarregaPlanos()
        {
            var lista = new List<SelectListItem>();
            var planos = planoDAO.BuscarTodos();
            foreach (var item in planos)
            {
                var option = new SelectListItem()
                {
                    Text = item.NomePlano
                    ,
                    Value = item.PlanoId.ToString()
                };
                lista.Add(option);
            }


            return lista;
        }

        /// <summary>
        /// Carrega Avaliações
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> CarregaAvaliacoes()
        {
            var lista = new List<SelectListItem>();
            //var avaliacoes = AvaliacaoContratoDAO.BuscarTodos();
            //foreach (var item in avaliacoes)
            //{
            //    var option = new SelectListItem()
            //    {
            //        Text = item.Descricao
            //        ,
            //        Value = item.AvaliacaoId.ToString()
            //    };
            //    lista.Add(option);
            //}

            lista.Add(new SelectListItem()
            {
                Text = "Boa"
                , Value = "1"
            });

            lista.Add(new SelectListItem()
            {
                Text = "Regular"
                ,Value = "2"
            });

            lista.Add(new SelectListItem()
            {
                Text = "Ruim"
                ,Value = "3"
            });


            return lista;
        }

        /// <summary>
        /// Busca View Model Por Busca
        /// </summary>
        /// <returns></returns>
        public List<AquisicaoContratoViewModel> BuscarViewModelporBusca()
        {
            var lista = new List<AquisicaoContratoViewModel>();

            var aquisicoes = aquisicaoDAO.BuscarTodos();


            foreach (var item in aquisicoes)
            {
                lista.Add(new AquisicaoContratoViewModel()
                {
                    

                });
                
            }

            return lista;

        }
    }


    
}

