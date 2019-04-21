using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
namespace EP.CursoMvc.Domain.Tests.Services
{
    [TestClass]
    public class ClienteServiceTests
    {
        [TestMethod]
        public void ClienteService_AdicionarCliente_RetornarComSucesso()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "Alberto",
                Email = "teste@teste.com",
                CPF = "25666158880",
                DataNascimento = new DateTime(1980, 01, 01)
            };

            var repo = MockRepository.GenerateStub<IClienteRepository>();

            var clienteService = new ClienteService(repo);

            // Act
            var result = clienteService.Adicionar(cliente);

            // Assert
            Assert.IsTrue(result.ValidationResult.IsValid);
        }

        [TestMethod]
        public void ClienteService_AdicionarCliente_RetornarComFalha()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "A",
                Email = "",
                CPF = "25666158881",
                DataNascimento = new DateTime(2019, 01, 01)
            };

            var repo = MockRepository.GenerateStub<IClienteRepository>();

            var clienteService = new ClienteService(repo);

            // Act
            var result = clienteService.Adicionar(cliente);

            // Assert
            Assert.IsFalse(result.ValidationResult.IsValid);
        }
    }
}
