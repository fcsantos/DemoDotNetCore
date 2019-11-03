using System;
using System.Threading.Tasks;
using testeEFCore.Business.Models;

namespace testeEFCore.Business.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
