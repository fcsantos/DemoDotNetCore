using System;
using System.Linq;
using System.Threading.Tasks;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Models;
using testeEFCore.Business.Models.Validations;

namespace testeEFCore.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository,
                              INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

            if (_produtoRepository.Buscar(p => p.Nome == produto.Nome).Result.Any())
            {
                Notificar("Já existe um produto com este nome informado.");
                return false;
            }

            await _produtoRepository.Adicionar(produto);
            return true;
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

            if (_produtoRepository.Buscar(p => p.Nome == produto.Nome && p.Id != produto.Id).Result.Any())
            {
                Notificar("Já existe um produto com este nome informado.");
                return false;
            }

            await _produtoRepository.Atualizar(produto);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
