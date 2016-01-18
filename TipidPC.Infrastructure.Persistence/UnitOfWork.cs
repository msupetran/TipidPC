using Common.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipidPC.Domain.Models;

namespace TipidPC.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        // Fields
        private ITipidPcContext _context;
        private IRepository<Item> _itemRepository;
        private IRepository<Header> _headerRepository;
        private bool disposed = false;

        // Properties
        public IRepository<Item> ItemRepository
        {
            get { return _itemRepository; }
            set { _itemRepository = value; }
        }
        public IRepository<Header> HeaderRepository
        {
            get { return _headerRepository; }
            set { _headerRepository = value; }
        }

        // Constructors
        public UnitOfWork(ITipidPcContext context)
        {
            _context = context;
        }

        // Methods
        public int Save()
        {
            return _context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
