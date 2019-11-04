using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Models;
using testeEFCore.Business.Notificacoes;
using testeEFCore.Data.Context;
using testeEFCore.ViewModels;

namespace testeEFCore.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        private readonly IProdutoService _produtoService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        public IList<Notificacao> _errorMensagens { get; set; }

        public string _errorException { get; set; }

        public CreateModel(INotificador notificador,
                           IProdutoService produtoService,
                           IFornecedorRepository fornecedorRepository,
                           IMapper mapper)
        {
            _notificador = notificador;
            _produtoService = produtoService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {            
            ViewData["Fornecedores"] = _fornecedorRepository.ObterTodos().Result.Select(a => new SelectListItem
                                                                            {
                                                                                Value = a.Id.ToString(),
                                                                                Text = string.Format("{0}-{1}", a.Documento, a.Nome)
                                                                            }).ToList();

            return Page();
        }

        [BindProperty]
        public ProdutoViewModel Produto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = await _produtoService.Adicionar(_mapper.Map<Produto>(Produto));
                if (result == false) { _errorMensagens = _notificador.ObterNotificacoes(); return null; }
            }
            catch (DbUpdateException ex)
            {
                _errorException = ex.Message;
            }

            return RedirectToPage("./Index");
        }
    }
}