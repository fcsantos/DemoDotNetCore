using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Notificacoes;
using testeEFCore.ViewModels;

namespace testeEFCore.Pages.Fornecedores
{
    public class DeleteModel : PageModel
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        public IList<Notificacao> _errorMensagens { get; set; }
        public string _errorException { get; set; }

        public DeleteModel(INotificador notificador,
                         IFornecedorService fornecedorService,
                         IFornecedorRepository fornecedorRepository,
                         IMapper mapper)
        {
            _notificador = notificador;
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public FornecedorViewModel Fornecedor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterPorId(id.Value));

            if (Fornecedor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterPorId(id.Value));

            if (Fornecedor != null)
            {
                var result = await _fornecedorService.Remover(Fornecedor.Id);
                if (result == false) { _errorMensagens = _notificador.ObterNotificacoes(); return null; }
            }

            return RedirectToPage("./Index");
        }
    }
}
