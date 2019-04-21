using DomainValidation.Validation;
using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Domain.Models;
using EP.CursoMvc.Domain.Specifications.Clientes;

namespace EP.CursoMvc.Domain.Validations.Clientes
{
    public class ClienteEstaAptoParaCadastroValidation : Validator<Cliente>
    {
        public ClienteEstaAptoParaCadastroValidation(IClienteRepository clienteRepository)
        {
            var clienteUnicoCPF = new ClienteDevePossuirCPFUnicoSpecification(clienteRepository);
            var clienteUnicoEmail = new ClienteDevePossuirEmailUnicoSpecification(clienteRepository);

            Add("clienteUnicoCPF", new Rule<Cliente>(clienteUnicoCPF, "Já existe um cliente com este CPF"));
            Add("clienteUnicoEmail", new Rule<Cliente>(clienteUnicoEmail, "Já existe um cliente com este E-mail"));
        }
    }
}
