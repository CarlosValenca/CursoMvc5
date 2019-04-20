using DomainValidation.Validation;
using EP.CursoMvc.Application.Interfaces;
using EP.CursoMvc.Application.ViewModels;
using EP.CursoMvc.Infra.CrossCutting.Filters;
using System;
using System.Web.Mvc;

namespace EP.CursoMvc.UI.Site.Controllers
{
    // Prefixo da rota
    [RoutePrefix("area-administrativa/gestao-clientes")]
    // Estou pedindo que o usuário esteja conectado no Identity para usar as actions abaixo
    [Authorize]
    public class ClientesController : BaseController
    {
        // LI : Listar, DE : Detalhes, IN : Incluir, ED : Editar, EX : Excluir
        private readonly IClienteAppService _clienteAppService;

        // Exemplo de como você pode definir as roles
        private const string role = "Admin,Gestor";

        public ClientesController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        // GET: Clientes
        // Com a sobrecarga temos a oportunidade de usar uma das duas rotas abaixo:
        // area-administrativa/gestao-clientes/clientes/listar-todos/index OU
        // area-administrativa/gestao-clientes/clientes/index
        [Route("")]
        [Route("listar-todos")]
        // Usuários conectados com a claim de Clientes e o valor LI podem listar
        [ClaimsAuthorize("Clientes", "LI")]
        public ActionResult Index()
        {
            return View(_clienteAppService.ObterAtivos());
        }

        // GET: Clientes/Details/5
        // Tirei o nullable do Guid ? pois não faz sentido passar null para retornar um cliente
        // A rota abaixo opcionalmente eu posso informar o tipo do id para proteger a aplicação de
        // text injections, evitando receber uma informação que vc não está preparado para tratar
        [Route("detalhes/{id:guid}")]
        [ClaimsAuthorize("Clientes", "DE")]
        public ActionResult Details(Guid id)
        {

            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null) return HttpNotFound();

            return View(clienteViewModel);
        }

        // GET: Clientes/Create
        [Route("criar-novo")]
        [ClaimsAuthorize("Clientes", "IN")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("criar-novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Clientes", "IN")]
        public ActionResult Create(ClienteEnderecoViewModel clienteEndereco)
        {
            if (!ModelState.IsValid) return View(clienteEndereco);

            clienteEndereco = _clienteAppService.Adicionar(clienteEndereco);

            if(clienteEndereco.Cliente.ValidationResult.IsValid) return RedirectToAction("Index");

            PopularModelStateComErros(clienteEndereco.Cliente.ValidationResult);

            return View(clienteEndereco);
        }

        // GET: Clientes/Edit/5
        [Route("{id:guid}/editar")]
        [ClaimsAuthorize("Clientes", "ED")]
        public ActionResult Edit(Guid id)
        {
            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null) return HttpNotFound();

            return View(clienteViewModel);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("{id:guid}/editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Clientes", "ED")]
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return View(clienteViewModel);

            _clienteAppService.Atualizar(clienteViewModel);

            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        [Route("{id:guid}/excluir")]
        // Além de estar conectado, estou pedindo ou a role Admin ou a Role Gestor para o usuário
        [Authorize(Roles = role)]
        [ClaimsAuthorize("Clientes", "EX")]
        public ActionResult Delete(Guid id)
        {
            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null) return HttpNotFound();

            return View(clienteViewModel);
        }

        // POST: Clientes/Delete/5
        [Route("{id:guid}/excluir")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Clientes", "EX")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _clienteAppService.Remover(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _clienteAppService.Dispose();
        }
    }
}
