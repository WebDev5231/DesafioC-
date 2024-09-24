using Consumer.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;

namespace Consumer
{
    public class ProtocoloRepository
    {
        private readonly string _connectionString;

        public ProtocoloRepository(string connectionString)
        {
            _connectionString = connectionString;
            CriarTabela();
        }

        private void CriarTabela()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Protocolos' AND xtype='U')
                CREATE TABLE Protocolos (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    NumeroProtocolo NVARCHAR(50),
                    NumeroVia INT,
                    Cpf NVARCHAR(11),
                    Rg NVARCHAR(10),
                    Nome NVARCHAR(100),
                    NomeMae NVARCHAR(100),
                    NomePai NVARCHAR(100),
                    Foto NVARCHAR(255)
                );";
            connection.Execute(sql);
        }

        public void AdicionarProtocolo(Protocolo protocolo)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Protocolos (NumeroProtocolo, NumeroVia, Cpf, Rg, Nome, NomeMae, NomePai, Foto) VALUES (@NumeroProtocolo, @NumeroVia, @Cpf, @Rg, @Nome, @NomeMae, @NomePai, @Foto)";
            connection.Execute(sql, protocolo);
        }
    }
}
