using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Linq;
using Dapper;

namespace Projeto.Core.Services
{
    public class ClienteService : ICrudService<Cliente>
    {

        private SQLiteConnection _connection;

        public void InjetarConexao(SQLiteConnection connection)
        {
            _connection = connection;
        }

        private bool Validate(Cliente entity)
        {
            // TODO: não deixe que clientes com o mesmo e-mail sejam inseridos na base de dados
            var email = GetByEmail(entity.Email);
            
            if (email != null)
            {
                //throw new BusinessException("O e-mail informado já está cadastrado");
                return false;
            }

            return true;
        }

        public void Delete(Cliente entity)
        {
            _connection.Query<Cliente>("DELETE * FROM cliente where id = @id", new { id = entity.Id });
        }

        public Cliente GetById(int id)
        {
            return _connection.Query<Cliente>("SELECT id, nome, data_nascimento as DataNascimento, email FROM cliente where id = @id", new { id }).SingleOrDefault();
        }

        public void Insert(Cliente entity)
        {
            if (Validate(entity))
            {
                _connection.Execute("INSERT INTO cliente (nome, data_nascimento, email) VALUES(@Nome, @DataNascimento. @Email)", entity);
            }

            throw new BusinessException("O e-mail informado já está cadastrado");
        }

        public void Update(Cliente entity)
        {
            _connection.Execute("UPDATE cliente SET nome = @Nome, data_nascimento = @DataNascimento, email = @Email WHERE id = @Id", entity);
        }

        public Cliente GetByEmail(string email)
        {
            return _connection.Query<Cliente>("SELECT * FROM cliente where email = @Email", new { email }).SingleOrDefault();
        }
    }
}
