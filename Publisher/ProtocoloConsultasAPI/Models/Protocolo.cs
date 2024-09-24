using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProtocoloConsultasAPI.Models
{
    public class Protocolo
    {
        [Required]
        public string NumeroProtocolo { get; set; }

        [Required]
        public int NumeroVia { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve ter 11 dígitos.")]
        public string Cpf { get; set; }

        [Required]
        public string Rg { get; set; }

        [Required]
        public string Nome { get; set; }

        public string NomeMae { get; set; }
        public string NomePai { get; set; }

        [Required]
        public byte[] Foto { get; set; }
    }
}