using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Specifications.Clientes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace EP.CursoMvc.Domain.Tests.Specifications
{
    [TestClass]
    public class CPFSpecificationTests
    {
        [TestMethod]
        public void CpfSpecification_Valido_True()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "Carlos",
                CPF = "66828849078",
                Email = "teste@teste.com",
                DataNascimento = new DateTime(1982, 01, 01),
                DataCadastro = DateTime.Now,
                Ativo = true,
                Excluido = false
            };

            var cpfSpec = new ClienteDeveTerCpfValidoSpecification();

            // Act
            var result = cpfSpec.IsSatisfiedBy(cliente);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CpfSpecification_Valido_False()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "C",
                CPF = "25666158881",
                Email = "",
                DataNascimento = new DateTime(2019, 01, 01),
                DataCadastro = DateTime.Now,
                Ativo = true,
                Excluido = false
            };

            var cpfSpec = new ClienteDeveTerCpfValidoSpecification();

            // Act
            var result = cpfSpec.IsSatisfiedBy(cliente);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
