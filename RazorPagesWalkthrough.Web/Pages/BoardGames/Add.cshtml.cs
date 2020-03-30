using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesWalkthrough.Web.DataContext;
using RazorPagesWalkthrough.Web.Models;

namespace RazorPagesWalkthrough.Web.Pages.BoardGames
{
    public class AddModel : PageModel
    {
        private readonly RazorPagesWalkthrough.Web.DataContext.BoardGamesDbContext _context;

        public AddModel(RazorPagesWalkthrough.Web.DataContext.BoardGamesDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BoardGame BoardGame { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {                         
                //Determine the next ID
                var newID = _context.BoardGames.Select(x => x.ID).Max() + 1;
                this.BoardGame.ID = newID;

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.BoardGames.Add(BoardGame);
                await _context.SaveChangesAsync();

                transaction.Commit();

                return RedirectToPage("./Index");
            }
        }
    }
}