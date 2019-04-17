using EP.CursoMvc.Application.Services;
using EP.CursoMvc.Application.ViewModels;
using System;
using System.Web.Mvc;

namespace EP.CursoMvc.UI.Site.Controllers
{
    // Prefixo da rota
    [RoutePrefix("area-administrativa/gestao-clientes")]
    // Estou pedindo que o usuário esteja conectado no Identity para usar as actions abaixo
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly ClienteAppService _clienteAppService;

        // Exemplo de como você pode definir as roles
        private const string role = "Admin,Gestor";

        public ClientesController()
        {
            _clienteAppService = new ClienteAppService();
        }

        // GET: Clientes
        // Com a sobrecarga temos a oportunidade de usar uma das duas rotas abaixo:
        // area-administrativa/gestao-clientes/clientes/listar-todos/index OU
        // area-administrativa/gestao-clientes/clientes/index
        [Route("")]
        [Route("listar-todos")]
        // Estou abrindo uma excessão para deixar usuários não conectados listar os clientes
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_clienteAppService.ObterAtivos());
        }

        // GET: Clientes/Details/5
        // Tirei o nullable do Guid ? pois não faz sentido passar null para retornar um cliente
        // A rota abaixo opcionalmente eu posso informar o tipo do id para proteger a aplicação de
        // text injections, evitando receber uma informação que vc não está preparado para tratar
        [Route("detalhes/{id:guid}")]
        public ActionResult Details(Guid id)
        {

            var clienteViewModel = _clienteAppService.ObterPorId(id);

            if (clienteViewModel == null) return HttpNotFound();

            return View(clienteViewModel);
        }

        // GET: Clientes/Create
        [Route("criar-novo")]
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
        public ActionResult Create(ClienteEnderecoViewModel clienteEndereco)
        {
            if (!ModelState.IsValid) return View(clienteEndereco);

            _clienteAppService.Adicionar(clienteEndereco);

            return RedirectToAction("Index");
        }

        // GET: Clientes/Edit/5
        [Route("{id:guid}/editar")]
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
        [Authorize(Roles = role)]
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
