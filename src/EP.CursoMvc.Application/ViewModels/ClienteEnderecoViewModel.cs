using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EP.CursoMvc.Domain.Models;

namespace EP.CursoMvc.Application.ViewModels
{
    public class ClienteEnderecoViewModel
    {
        public ClienteViewModel Cliente { get; set; }
        public EnderecoViewModel Endereco { get; set; }
    }
}
