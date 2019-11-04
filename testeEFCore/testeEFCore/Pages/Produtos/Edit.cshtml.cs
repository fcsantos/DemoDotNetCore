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
    public class EditModel : PageModel
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        public IList<Notificacao> _errorMensagens { get; set; }
        public string _errorException { get; set; }

        public EditModel(INotificador notificador,
                           IProdutoService produtoService,
                           IProdutoRepository produtoRepository,
                           IFornecedorRepository fornecedorRepository,
                           IMapper mapper)
        {
            _notificador = notificador;
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public ProdutoViewModel Produto { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id.Value));

            if (Produto == null)
            {
                return NotFound();
            }

            ViewData["Fornecedores"] = _fornecedorRepository.ObterTodos().Result.Select(a => new SelectListItem
                                                                                        {
                                                                                            Value = a.Id.ToString(),
                                                                                            Text = string.Format("{0}-{1}", a.Documento, a.Nome)
                                                                                        }).ToList();
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
                Produto.Valor = (decimal)Produto.Valor;
                var result = await _produtoService.Atualizar(_mapper.Map<Produto>(Produto));
                if (result == false) { _errorMensagens = _notificador.ObterNotificacoes(); return null; }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProdutoExists(Produto.Id))
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

        private bool ProdutoExists(Guid id)
        {
            return _produtoRepository.Buscar(f => f.Id == id).Result.Any();
        }
    }
}
