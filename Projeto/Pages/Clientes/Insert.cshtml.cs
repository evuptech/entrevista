using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Services;
using System.Data.SQLite;

namespace Projeto.Pages.Clientes
{
    public class InsertModel : PageModel
    {
        private readonly ICrudService<Cliente> _service;

        public InsertModel(ICrudService<Cliente> service)
        {
            _service = service;
        }

        [BindProperty]
        public Cliente cliente { get; set; }

        public void OnGet()
        {
            // TODO: cadastro de novo cliente não implementado
        }

        public ActionResult OnPost()
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            ((ClienteService)_service).InjetarConexao(connection);
                        
            _service.Insert(cliente);

            return Page();
        }
    }
}
