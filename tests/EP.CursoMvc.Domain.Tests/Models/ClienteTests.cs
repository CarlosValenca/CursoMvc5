using EP.CursoMvc.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EP.CursoMvc.Domain.Tests.Models
{
    [TestClass]
    public class ClienteTests
    {
        // AAA => Arrange, Act, Assert
        // Arrange: arranjar o estado do seu objeto (Cliente)
        // Act: Ação que vc está testando (EstaConsistente)
        // Assert: = Validar se o resultado do seu teste está OK (DeveRetornarTrue)
        [TestMethod]
        public void Cliente_EstaConsistente_DeveRetornarTrue()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "Alysson",
                CPF = "15743417075",
                Email = "teste@teste.com",
                DataNascimento = new DateTime(1980, 01, 01)
            };

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Cliente_EstaConsistente_DeveRetornarFalse()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nome = "C",
                CPF = "25666158881",
                Email = "carlos_valenca",
                DataNascimento = new DateTime(2019, 01, 01)
            };

            // Act
            var result = cliente.EhValido();

            // Assert
            // Estou confirmando se a inclusão de uma forma geral funciona
            Assert.IsFalse(result);

            // Estou confirmando se deu erro pelos seguintes motivos
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente informou um CPF inválido"));
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente informou um E-mail inválido"));
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente precisa ser maior de idade"));
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "O nome do cliente precisa ter mais de 2 caracteres"));
        }
    }
}
