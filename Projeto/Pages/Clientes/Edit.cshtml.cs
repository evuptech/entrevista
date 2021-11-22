using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Services;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Pages.Clientes
{

    public class EditModel : PageModel
    {
        private readonly ICrudService<Cliente> _service;

        public EditModel(ICrudService<Cliente> service)
        {
            _service = service;
        }

        [BindProperty]
        public Cliente Cliente { get; set; }

        public string Erro { get; set; }
        public string Message { get; set; }

        public void OnGet(int id)
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            //this.Cliente = connection.Query<Cliente>("SELECT id, nome, data_nascimento as dataNascimento, email FROM cliente").FirstOrDefault();
            ((ClienteService)_service).InjetarConexao(connection);
            this.Cliente = _service.GetById(id);

        }

        //public IActionResult OnPost(int id)
        public IActionResult OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        using var connection = new SQLiteConnection($"Data Source=banco.db");

                        //connection.Execute("UPDATE cliente SET nome = @Nome, email = @Email WHERE id = @Id", this.Cliente);
                        ((ClienteService)_service).InjetarConexao(connection);
                        _service.Update(this.Cliente);

                        // TODO: exibir mensagem de "Registro atualizado com sucesso."
                        this.Message = "Registro atualizado com sucesso.";
                        return RedirectToPage("./Index");
                    }
                    catch(System.Exception e)
                    {
                        this.Erro = e.Message;
                    }
                }
                //return Page();
            }
            catch (System.Exception ex)
            {
                this.Erro = ex.Message;
            }

            return Page();
        }
    }
}
