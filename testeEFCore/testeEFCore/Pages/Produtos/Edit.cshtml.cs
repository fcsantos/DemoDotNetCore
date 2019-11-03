using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testeEFCore.Business.Models;
using testeEFCore.Data.Context;

namespace testeEFCore.Pages.Produtos
{
    public class EditModel : PageModel
    {
        private readonly testeEFCore.Data.Context.MeuDbContext _context;

        public EditModel(testeEFCore.Data.Context.MeuDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produto Produto { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto = await _context.Produtos
                .Include(p => p.Fornecedor).FirstOrDefaultAsync(m => m.Id == id);

            if (Produto == null)
            {
                return NotFound();
            }
           ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Documento");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(Produto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
