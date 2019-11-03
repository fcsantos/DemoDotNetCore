using System;
using System.Threading.Tasks;
using testeEFCore.Business.Models;

namespace testeEFCore.Business.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
