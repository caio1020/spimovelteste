using Dapper;
using Microsoft.Extensions.Configuration;
using SistemaClienteTeste.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IConfiguration _configuration;

        public ClienteRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DataBase"));
        }

        public async Task<ClienteModel> BuscarPorID(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ClienteModel>("sp_listarClientePorId", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<ClienteModel>> BuscarTodos()
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<ClienteModel>("sp_listarTodosClientes")).ToList();
            }
        }

        public async Task<ClienteModel> Adicionar(ClienteModel cliente)
        {
            var param = new {
                Nome = cliente.Cliente,
                TipoCliente = cliente.TipoCliente,
                NomeContato = cliente.NomeContato,
                TelefoneContato = cliente.TelefoneConato,
                Cidade = cliente.Cidade,
                Bairro = cliente.Bairro,
                Logradouro = cliente.Logradouro
            };

            using (var connection = CreateConnection())
            {
               await connection.ExecuteAsync("sp_cadastrarCliente", param, commandType: CommandType.StoredProcedure);

                return cliente;
            }
        }

        public async Task<ClienteModel> Atualizar(ClienteModel cliente)
        {
            ClienteModel clienteDB = await BuscarPorID(cliente.ClienteId);

            if (clienteDB == null) throw new Exception("Houve um erro na atualização do cliente!");

            var param = new {
                Id = cliente.ClienteId,
                Nome = cliente.Cliente,
                TipoCliente = cliente.TipoCliente,
                NomeContato = cliente.NomeContato,
                TelefoneContato = cliente.TelefoneConato,
                Cidade = cliente.Cidade,
                Bairro = cliente.Bairro,
                Logradouro = cliente.Logradouro
            };

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync("sp_atualizarCliente", param, commandType: CommandType.StoredProcedure);

                return cliente;
            }
        }

        public async Task<bool> Apagar(int id)
        {
            ClienteModel clienteDB = await BuscarPorID(id);

            if (clienteDB == null) throw new Exception("Houve um erro na deleção do cliente!");

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync("sp_deletarClientePorId", new { Id = id}, commandType: CommandType.StoredProcedure);

                return true;
            }
        }
    }
}
