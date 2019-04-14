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
            return Buscar(c => c.Ativo);
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
        public override Cliente ObterPorId(Guid Id)
        {
            // AsNoTracking : Não cria o tracking pelo EF, melhora a performance
            return Db.Clientes.AsNoTracking().Include("Endereco").FirstOrDefault(c => c.Id == Id);
        }

        public override void Remover(Guid id)
        {
            var cliente = ObterPorId(id);
            cliente.DefinirComoExcluido();

            Atualizar(cliente);
        }
    }
}
