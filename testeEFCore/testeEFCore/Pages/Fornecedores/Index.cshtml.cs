using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Models;
using testeEFCore.Data.Context;
using testeEFCore.ViewModels;

namespace testeEFCore.Pages.Fornecedores
{
    public class IndexModel : PageModel
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public IndexModel(IFornecedorRepository fornecedorRepository,
                          IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public IList<FornecedorViewModel> Fornecedor { get;set; }

        public async Task OnGetAsync()
        {
            Fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()).ToList();
        }
    }
}
