using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entitys;
using Talabat.Core.IRepositiry;
using Talabat.Repositiry.Data;
using Talabat.Repositiry.Repositiry;

namespace Talabat.Repositiry
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        private Hashtable repostitry; 
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            repostitry = new Hashtable(); 
        }
        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }

        public async Task<int> IsComplet()
        {
            return await context.SaveChangesAsync();
        }

        public IBaseRepositiry<T> Repostitry<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            if (!repostitry.ContainsKey(type))
            {
                repostitry.Add(type, new BaseRepositiry<T>(context)); 
            }
            return repostitry[type] as IBaseRepositiry<T>; 
        }
    }
}
