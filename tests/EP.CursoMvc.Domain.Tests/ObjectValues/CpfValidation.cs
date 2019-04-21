using EP.CursoMvc.Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EP.CursoMvc.Domain.Tests.ObjectValues
{
    [TestClass]
    public class CpfValidation
    {
        [TestMethod]
        public void CPF_Valido_True()
        {
            // Arrange
            var CPFTest = "25666158880";

            // Act
            var result = CPF.Validar(CPFTest);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        // O DataRow substitui o Assert, aqui estou testando várias possibilidades de CPF Inválido
        [DataRow("256.661.588-80")]
        [DataRow("25666158881")]
        [DataRow("11111111111")]
        [DataRow("111111111111")]
        [DataRow("1")]
        public void CPF_Valido_False(string cpf)
        {
            // Act
            var result = CPF.Validar(cpf);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
