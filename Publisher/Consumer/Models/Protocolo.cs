using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Models
{
    public class Protocolo
    {
        public string NumeroProtocolo { get; set; }
        public int NumeroVia { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Nome { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string Foto { get; set; }
    }
}

//    CREATE TABLE Protocolos(
//    Id INT IDENTITY(1,1) PRIMARY KEY,
//    NumeroProtocolo NVARCHAR(50),
//    NumeroVia INT,
//    Cpf NVARCHAR(11),
//    Rg NVARCHAR(10),
//    Nome NVARCHAR(100),
//    NomeMae NVARCHAR(100),
//    NomePai NVARCHAR(100),
//    Foto NVARCHAR(255)
//);
