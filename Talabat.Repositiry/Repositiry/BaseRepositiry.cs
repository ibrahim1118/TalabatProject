using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.IRepositiry;
using Talabat.Core.Spcifications;
using Talabat.Repositiry.Data;
using Talabat.Repositiry.Spacification;

namespace Talabat.Repositiry.Repositiry
{
    public class BaseRepositiry<T> : IBaseRepositiry<T> where T : BaseEntity
    {
        private readonly AppDbContext context;

        public BaseRepositiry(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAysnc(T itme)
        {
            await context.Set<T>().AddAsync(itme);
            
        }

        public async Task Delete(T item)
        {
            context.Set<T>().Remove(item); 
            
        }

        public async Task<IReadOnlyList<T>> GetAllAysnc()
        {
          /*  if (typeof(T) == typeof(Product))
                return (IReadOnlyList<T>) await context.Products.Include(p => p.productType).Include(p => p.productBrand).ToListAsync();*/ 
            return await context.Set<T>().ToListAsync();   
        }

        public async Task<T> GetByIdAysnc(int id)
        {
           return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdSpacAysnc(ISpacification<T> spac)
        {
           return await SpacificationCreation<T>.CreatSpacification(context.Set<T>() , spac).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpac(ISpacification<T> spac)
        {
            return await SpacificationCreation<T>.CreatSpacification(context.Set<T>(), spac).CountAsync(); 
        }

        public async Task<IReadOnlyList<T>> GetSpacAllAysnc(ISpacification<T> spac)
        {
            return await SpacificationCreation<T>.CreatSpacification(context.Set<T>(), spac).ToListAsync();

        }

        public async Task Update(T itme)
        {
            context.Set<T>().Update(itme);
        }
    }
}
