using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaClienteTeste.Models
{
    public class ClienteModel
    {
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Digite o nome do cliente")]
        public string Cliente { get; set; }
        [Required(ErrorMessage = "Digite o tipo do cliente")]
        public string TipoCliente { get; set; }        
        public string NomeContato { get; set; }
        public string TelefoneConato { get; set; }
        [Required(ErrorMessage = "Digite a cidade")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Digite o bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite o logradouro")]
        public string Logradouro { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
