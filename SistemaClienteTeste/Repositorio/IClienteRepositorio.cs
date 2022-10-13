using SistemaClienteTeste.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Repositorio
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteModel>> BuscarTodos();
        Task<ClienteModel> BuscarPorID(int id);
        Task<ClienteModel> Adicionar(ClienteModel contato);
        Task<ClienteModel> Atualizar(ClienteModel contato);
        Task<bool> Apagar (int id);
    }
}
