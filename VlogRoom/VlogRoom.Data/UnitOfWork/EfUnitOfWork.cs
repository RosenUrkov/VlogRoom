using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VlogRoom.Data.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly MsSqlDbContext context;

        public EfUnitOfWork(MsSqlDbContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
