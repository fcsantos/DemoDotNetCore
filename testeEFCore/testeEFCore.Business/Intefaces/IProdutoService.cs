using System;
using System.Threading.Tasks;
using testeEFCore.Business.Models;

namespace testeEFCore.Business.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task<bool> Adicionar(Produto produto);
        Task<bool> Atualizar(Produto produto);
        Task<bool> Remover(Guid id);
    }
}
