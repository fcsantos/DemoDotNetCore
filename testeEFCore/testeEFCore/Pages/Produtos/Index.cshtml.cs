using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testeEFCore.Business.Intefaces;
using testeEFCore.ViewModels;

namespace testeEFCore.Pages.Produtos
{
    public class IndexModel : PageModel
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public string Mensagem { get; set; }

        public IndexModel(IProdutoRepository produtoRepository,
                          IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public IList<ProdutoViewModel> Produto { get;set; }

        public async Task OnGetAsync()
        {
            Produto = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()).ToList();
        }
    }
}
