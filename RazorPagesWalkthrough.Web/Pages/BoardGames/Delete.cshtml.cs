using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWalkthrough.Web.DataContext;
using RazorPagesWalkthrough.Web.Models;

namespace RazorPagesWalkthrough.Web
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWalkthrough.Web.DataContext.BoardGamesDbContext _context;

        public DeleteModel(RazorPagesWalkthrough.Web.DataContext.BoardGamesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BoardGame BoardGame { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BoardGame = await _context.BoardGames.FirstOrDefaultAsync(m => m.ID == id);

            if (BoardGame == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BoardGame = await _context.BoardGames.FindAsync(id);

            if (BoardGame != null)
            {
                _context.BoardGames.Remove(BoardGame);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
