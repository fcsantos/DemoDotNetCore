using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Models;
using testeEFCore.Business.Notificacoes;
using testeEFCore.ViewModels;

namespace testeEFCore.Pages.Fornecedores
{
    public class CreateModel : PageModel
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        public IList<Notificacao> _errorMensagens { get; set; }

        public CreateModel(INotificador notificador,
                           IFornecedorService fornecedorService,
                           IMapper mapper)
        {
            _notificador = notificador;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FornecedorViewModel Fornecedor { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(Fornecedor));
            if (result == false) { _errorMensagens = _notificador.ObterNotificacoes(); return null; }

            return RedirectToPage("./Index");
        }
    }
}