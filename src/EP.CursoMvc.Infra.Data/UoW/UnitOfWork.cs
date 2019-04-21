using EP.CursoMvc.Domain.Interfaces;
using EP.CursoMvc.Infra.Data.Context;
using System;
namespace EP.CursoMvc.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CursoMvcContext _context;

        public UnitOfWork(CursoMvcContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Rollback()
        {
            _context.Database.CurrentTransaction.Rollback();
        }
        public void Commit()
        {
            _context.Database.CurrentTransaction.Commit();
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
