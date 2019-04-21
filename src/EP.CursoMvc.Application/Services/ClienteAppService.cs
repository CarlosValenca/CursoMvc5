using AutoMapper;
using DomainValidation.Validation;
using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Application.ViewModels;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using System;
using System.Collections.Generic;

namespace EP.CursoMvc.Application.Services
{
    // O que eu deixarei ser executado através de requests da camada de apresentação ?
    public class ClienteAppService : AppServiceBase, IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;

        public ClienteAppService(IClienteRepository clienteRepository,
                                 IClienteService clienteService,
                                 IUnitOfWork uow) : base(uow)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        public IEnumerable<ClienteViewModel> ObterAtivos()
        {
            // Trouxe do banco todos os clientes ativos
            // Fiz o mapeamento desta lista para a ClienteViewModel
            // Usando para isto a coleção IEnumerable
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterAtivos());
        }

        public ClienteViewModel ObterPorCpf(string cpf)
        {
            // Trouxe do banco um registro por CPF
            // Fiz o mapeamento deste registro na ClienteViewModel
            // Não precisei usar uma coleção do tipo IEnumerable
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorCpf(cpf));
        }

        public ClienteViewModel ObterPorEmail(string email)
        {
            // Trouxe do banco um registro por email
            // Fiz o mapeamento deste registro na ClienteViewModel
            // Não precisei usar uma coleção do tipo IEnumerable
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorEmail(email));
        }

        public ClienteViewModel ObterPorId(Guid id)
        {
            // Trouxe do banco um registro por id
            // Fiz o mapeamento deste registro na ClienteViewModel
            // Não precisei usar uma coleção do tipo IEnumerable
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorId(id));
        }

        public IEnumerable<ClienteViewModel> ObterTodos()
        {
            // Trouxe do banco todos os clientes
            // Fiz o mapeamento desta lista para a ClienteViewModel
            // Usando para isto a coleção IEnumerable
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterTodos());
        }

        public ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            // Aqui estamos extraindo o cliente e o endereço de domínio
            var cliente = Mapper.Map<Cliente>(clienteEnderecoViewModel.Cliente);
            var endereco = Mapper.Map<Endereco>(clienteEnderecoViewModel.Endereco);

            cliente.DefinirComoAtivo();
            cliente.AdicionarEndereco(endereco);

            var clienteReturn = _clienteService.Adicionar(cliente);

            //BeginTransaction();
            //// chamar proc de banco etc...
            //try
            //{
            //    Commit();
            //}
            //catch (Exception)
            //{
            //    Rollback();
            //}

            if(clienteReturn.ValidationResult.IsValid)
            {
                if(!SaveChanges())
                {
                    AdicionarErrosValidacao(cliente.ValidationResult,"Ocorreu um erro no momento de salvar os dados no banco.");
                }
            }

            // Em caso de erros estou devolvendo eles para a camada de apresentação na classe ClienteViewModel
            clienteEnderecoViewModel.Cliente.ValidationResult = clienteReturn.ValidationResult;

            return clienteEnderecoViewModel;
        }

        public ClienteViewModel Atualizar(ClienteViewModel clienteViewModel)
        {
            // Aqui estamos extraindo o cliente de domínio
            var cliente = Mapper.Map<Cliente>(clienteViewModel);
            if (!cliente.EhValido()) return clienteViewModel;
            _clienteService.Atualizar(cliente);
            return clienteViewModel;
        }

        public void Remover(Guid id)
        {
            _clienteService.Remover(id);
        }
        public void Dispose()
        {
            // Controller terminou de usar, mata app service que mata o repository que mata o contexto do banco de dados
            // para que a memória fique melhor gerenciada
            _clienteRepository.Dispose();
        }

    }
}