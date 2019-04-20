using EP.CursoMvc.Domain.Validations.Clientes;
using System;
using System.Collections.Generic;

namespace EP.CursoMvc.Domain.Models
{
    public class Cliente : Entity
    {
        public Cliente()
        {
            Enderecos = new List<Endereco>();
        }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (!endereco.EhValido())
            {
                AdicionarErrosValidacao(endereco.ValidationResult);
                return;
            }

            Enderecos.Add(endereco);
        }

        public void DefinirComoExcluido()
        {
            Ativo = false;
            Excluido = true;
        }

        public void DefinirComoAtivo()
        {
            Ativo = true;
            Excluido = false;
        }

        public override bool EhValido()
        {
            ValidationResult = new ClienteEstaConsistenteValidation().Validate(this);

            return ValidationResult.IsValid;

        }
    }
}
