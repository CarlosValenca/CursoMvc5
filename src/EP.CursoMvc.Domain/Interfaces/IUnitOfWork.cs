namespace EP.CursoMvc.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        // Este é para o EF - true/false : determina se os registros pendentes para envio no banco estão ou não salvos
        bool SaveChanges();
        void BeginTransaction();
        void Rollback();
        // Este é um commit manual
        void Commit();
    }
}
