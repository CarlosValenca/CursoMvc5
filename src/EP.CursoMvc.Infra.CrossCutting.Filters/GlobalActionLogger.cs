using System.Web.Mvc;

namespace EP.CursoMvc.Infra.CrossCutting.Filters
{
    public class GlobalActionLogger : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // Log de Auditoria

            if(filterContext.Exception != null)
            {
                // Manipular a Exception
                // Injetar alguma LIB de tratamento de erro
                // -> Gravar log de erro no BD
                // -> Email para o admin
                // -> Retornar cod de erro amigável

                // SEMPRE USE ASYNC AQUI DENTRO

                // Estou dizendo para o IIS que estou interceptando esta exception
                filterContext.ExceptionHandled = true;
                // Retorno um código 500 para que este erro vá para a página de erro
                filterContext.Result = new HttpStatusCodeResult(500);
            }

            base.OnResultExecuted(filterContext);
        }
    }
}