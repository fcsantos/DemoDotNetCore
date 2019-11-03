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

namespace testeEFCore.Pages.Fornecedores
{
    public class EditModel : PageModel
    {
        private readonly testeEFCore.Data.Context.MeuDbContext _context;

        public EditModel(testeEFCore.Data.Context.MeuDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Fornecedor Fornecedor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(m => m.Id == id);

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

            _context.Attach(Fornecedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(Fornecedor.Id))
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

        private bool FornecedorExists(Guid id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
