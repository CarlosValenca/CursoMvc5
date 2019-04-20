using System;

namespace EP.CursoMvc.Domain.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid ClienteId { get; set; }

        // Propriedade de Navegação do EF
        public virtual Cliente Cliente { get; set; }

        public override bool EhValido()
        {
            // Ficará assim temporariamente
            return true;
        }
    }
}
