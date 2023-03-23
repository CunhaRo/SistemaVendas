using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sis_RosangelaCunha.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string Ie { get; set; }
        public string ResponsavelEmpresa { get; set; }
        public string CpfResponsavel { get; set; }
        public string Cidade { get; set; }
        public enumEstado EnumEstado { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Celular { get; set; }

    }
}