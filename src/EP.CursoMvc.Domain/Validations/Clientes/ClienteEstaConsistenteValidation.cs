using DomainValidation.Validation;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Specifications;
using EP.CursoMvc.Domain.Specifications.Clientes;
using EP.CursoMvc.Domain.Value_Objects;

namespace EP.CursoMvc.Domain.Validations.Clientes
{
    public class ClienteEstaConsistenteValidation : Validator<Cliente>
    {
        public ClienteEstaConsistenteValidation()
        {
            //var CPFCliente = new ClienteDeveTerCpfValidoSpecification();
            var clienteEmail = new ClienteDeveTerEmailValidoSpecification();
            var clienteMaiorIdade = new ClienteDeveSerMaiorDeIdadeSpecification();

            // Aqui temos exemplos usando uma classe bem genérica, detalhes em GenericSpecification
            var clienteNomeCurto = new GenericSpecification<Cliente>(c => c.Nome.Length >= 2);
            var CPFCliente = new GenericSpecification<Cliente>(c => CPF.Validar(c.CPF));

            Add("CPFCliente", new Rule<Cliente>(CPFCliente, "Cliente informou um CPF inválido"));
            Add("clienteEmail", new Rule<Cliente>(clienteEmail, "Cliente informou um E-mail inválido"));
            Add("clienteMaiorIdade", new Rule<Cliente>(clienteMaiorIdade, "Cliente precisa ser maior de idade"));
            Add("clienteNomeCurto", new Rule<Cliente>(clienteNomeCurto, "O nome do cliente precisa ter mais de 2 caracteres"));
        }
    }
}
