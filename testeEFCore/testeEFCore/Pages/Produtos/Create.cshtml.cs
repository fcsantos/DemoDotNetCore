using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using testeEFCore.Business.Models;
using testeEFCore.Data.Context;

namespace testeEFCore.Pages.Produtos
{
    public class CreateModel : PageModel
    {
        private readonly testeEFCore.Data.Context.MeuDbContext _context;

        public CreateModel(testeEFCore.Data.Context.MeuDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Documento");
            return Page();
        }

        [BindProperty]
        public Produto Produto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}