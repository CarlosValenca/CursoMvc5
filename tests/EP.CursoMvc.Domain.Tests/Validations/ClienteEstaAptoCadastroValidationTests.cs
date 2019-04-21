using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Validations.Clientes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Linq;

namespace EP.CursoMvc.Domain.Tests.Validations
{
    [TestClass]
    public class ClienteEstaAptoCadastroValidationTests
    {
        [TestMethod]
        public void ClienteAptoCadastro_Validation_DeveRetornarTrue()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "Alysson",
                CPF = "15743417075",
                Email = "teste@teste.com",
                DataNascimento = new DateTime(1980, 01, 01)
            };

            // MOQ : Fingir ser um objeto como um repositório que não retorna nada
            var repo = MockRepository.GenerateStub<IClienteRepository>();

            // Estou dizendo ao método ObterPorEmail que ao passar o email teste@teste.com o resultado será nulo
            // no intuito de dizer que este email não está cadastrado no banco deixando assim cadastrar o cliente
            repo.Stub(s => s.ObterPorEmail(cliente.Email)).Return(null);

            // Estou dizendo ao método ObterPorCpf que ao passar o cpf 15743417075 o resultado será nulo
            // no intuito de dizer que este CPF não está cadastrado no banco deixando assim cadastrar o cliente
            repo.Stub(s => s.ObterPorCpf(cliente.CPF)).Return(null);

            var cliValidation = new ClienteEstaAptoParaCadastroValidation(repo);

            // Act
            var result = cliValidation.Validate(cliente);

            // Assert
            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void ClienteAptoCadastro_Validation_DeveRetornarFalse()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "Alberto",
                CPF = "25666158880",
                Email = "carlos_valenca@uol.com.br",
                DataNascimento = new DateTime(1980, 01, 01)
            };

            // MOQ : Fingir ser um objeto como um repositório que não retorna nada
            var repo = MockRepository.GenerateStub<IClienteRepository>();

            // Estou dizendo ao método ObterPorEmail que ao passar o email teste@teste.com  irá achar um cliente
            // A idéia aqui
            repo.Stub(s => s.ObterPorEmail(cliente.Email)).Return(cliente);

            // Estou dizendo ao método ObterPorCpf que ao passar o cpf 25666158880 irá achar um cliente
            repo.Stub(s => s.ObterPorCpf(cliente.CPF)).Return(cliente);

            var cliValidation = new ClienteEstaAptoParaCadastroValidation(repo);

            // Act
            var result = cliValidation.Validate(cliente);

            // Assert
            Assert.IsFalse(result.IsValid);

            // Para ter certeza que falhou pelos motivos esperados
            Assert.IsTrue(result.Erros.Any(e => e.Message == "Já existe um cliente com este CPF"));
            Assert.IsTrue(result.Erros.Any(e => e.Message == "Já existe um cliente com este E-mail"));
        }
    }
}
