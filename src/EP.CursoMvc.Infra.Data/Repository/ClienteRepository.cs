using Dapper;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.CursoMvc.Infra.Data.Repository
{
    // Cliente Repository faz tudo que o Repository faz + as especificidades desta classe
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public IEnumerable<Cliente> ObterAtivos()
        {
            // 0 = False; 1 = True;
            var sql =  @"select * " +
                        "from clientes c " +
                        "where c.excluido = 0 and c.ativo = 1";

            // Db.Database.Connection pega emprestado a conexão aberta pelo EF
            // o Query já é do dapper
            // Estamos usando o dapper para aumentar a performance das
            // consultas em relação ao EF
            return Db.Database.Connection.Query<Cliente>(sql);

            // return Buscar(c => c.Ativo);
        }

        public Cliente ObterPorCpf(string cpf)
        {
            return Buscar(c => c.CPF == cpf).FirstOrDefault();
        }

        public Cliente ObterPorEmail(string email)
        {
            return Buscar(c => c.Email == email).FirstOrDefault();
        }

        // Aqui ao invés de retornar somente o cliente pelo método da super classe retornaremos o cliente junto com o endereço
        public override Cliente ObterPorId(Guid id)
        {
            // Consulte explanations.txt para maiores detalhes sobre o EF e o Dapper

            var sql = @"select * from clientes c left join enderecos e " +
                       "on c.id = e.clienteid and c.id = @uid and c.excluido = 0 and c.ativo = 1";

            return Db.Database.Connection.Query<Cliente, Endereco, Cliente>(sql,
                (c, e) =>
                {
                    c.AdicionarEndereco(e);
                    return c;
                }, new { uid = id }).FirstOrDefault();
        }

        public override void Remover(Guid id)
        {
            var cliente = ObterPorId(id);
            cliente.DefinirComoExcluido();

            Atualizar(cliente);
        }
    }
}
