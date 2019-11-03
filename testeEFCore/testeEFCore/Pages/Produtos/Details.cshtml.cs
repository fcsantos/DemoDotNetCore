using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using testeEFCore.Business.Models;
using testeEFCore.Data.Context;

namespace testeEFCore.Pages.Produtos
{
    public class DetailsModel : PageModel
    {
        private readonly testeEFCore.Data.Context.MeuDbContext _context;

        public DetailsModel(testeEFCore.Data.Context.MeuDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
