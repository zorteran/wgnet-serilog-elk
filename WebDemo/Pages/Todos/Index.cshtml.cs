using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebDemo.Models;

namespace WebDemo.Pages.Todos
{
    public class IndexModel : PageModel
    {
        private readonly WebDemo.Models.AwesomeDbContext _context;

        public IndexModel(WebDemo.Models.AwesomeDbContext context)
        {
            _context = context;
        }

        public IList<Todo> Todo { get;set; }

        public async Task OnGetAsync()
        {
            Todo = await _context.Todos.ToListAsync();
        }
    }
}
