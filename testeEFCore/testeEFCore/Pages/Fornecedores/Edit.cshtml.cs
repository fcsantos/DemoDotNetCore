using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Models;
using testeEFCore.Business.Notificacoes;
using testeEFCore.ViewModels;

namespace testeEFCore.Pages.Fornecedores
{
    public class EditModel : PageModel
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        public IList<Notificacao> _errorMensagens { get; set; }
        public string _errorException { get; set; }

        public EditModel(INotificador notificador,
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {                
                var result = await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(Fornecedor));
                if (result == false) { _errorMensagens = _notificador.ObterNotificacoes(); return null; }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FornecedorExists(Fornecedor.Id))
                {
                    return NotFound();
                }
                else
                {
                    _errorException = ex.Message;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FornecedorExists(Guid id)
        {
            return _fornecedorRepository.Buscar(f => f.Id == id).Result.Any();
        }
    }
}
