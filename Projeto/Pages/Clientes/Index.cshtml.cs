using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto.Core.Entity;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<Cliente> Clientes { get; set; }
        public void OnGet()
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            this.Clientes = connection.Query<Cliente>("SELECT id, nome, data_nascimento as DataNascimento, email FROM cliente").ToList();

        }
    }
}
