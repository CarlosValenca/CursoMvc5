using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using System;
namespace EP.CursoMvc.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Cliente Adicionar(Cliente cliente)
        {
            if (!cliente.EhValido()) return cliente;

            _clienteRepository.Adicionar(cliente);
            return cliente;
        }

        public Cliente Atualizar(Cliente cliente)
        {
            if (!cliente.EhValido()) return cliente;

            _clienteRepository.Atualizar(cliente);
            return cliente;
        }

        public void Remover(Guid id)
        {
            _clienteRepository.Remover(id);
        }
        public void Dispose()
        {
            _clienteRepository.Dispose();
        }
    }
}
