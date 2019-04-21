using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EP.CursoMvc.REST.ClienteAPI.Controllers
{
    // Aceita requests de qualquer origem, header e métodos, este é um exemplo de política determinada manualmente
    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    // Aqui estamos usando uma classe para determinar a política de acessos externos a esta aplicação
    [MyCorsPolicy]
    public class ClientesController : ApiController
    {
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        [HttpGet]
        public IEnumerable<ClienteViewModel> ObterClientes()
        {
            return _clienteAppService.ObterAtivos();
        }

        [HttpGet]
        public ClienteViewModel ObterPorId(Guid id)
        {
            return _clienteAppService.ObterPorId(id);
        }

        [HttpPost]
        public IHttpActionResult Adicionar([FromBody]ClienteEnderecoViewModel clienteEndereco)
        {
            // 400 - Não é obrigatório, é só trocar o IHttpActionRequest por void
            if (!ModelState.IsValid) return BadRequest();

            _clienteAppService.Adicionar(clienteEndereco);

            // 200
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Atualizar(Guid id, [FromBody]ClienteViewModel cliente)
        {
            // 400 - Não é obrigatório, é só trocar o IHttpActionRequest por void
            if (!ModelState.IsValid) return BadRequest();

            _clienteAppService.Atualizar(cliente);
            // 200
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Remover(Guid id)
        {
            _clienteAppService.Remover(id);

            // 200
            return Ok();
        }
    }
}
