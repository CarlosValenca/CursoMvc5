using DomainValidation.Validation;
using EP.CursoMvc.Domain.Interfaces;
namespace EP.CursoMvc.Application.Interfaces
{
    public abstract class AppServiceBase
    {
        private readonly IUnitOfWork _uow;

        protected AppServiceBase(IUnitOfWork uow)
        {
            _uow = uow;
        }
        protected void AdicionarErrosValidacao(ValidationResult validationResult, string erro)
        {
            validationResult.Add(new ValidationError(erro));
        }
        protected bool SaveChanges()
        {
            return _uow.SaveChanges();
        }
        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.Commit();
        }

        public void Rollback()
        {
            _uow.Rollback();
        }
    }
}
