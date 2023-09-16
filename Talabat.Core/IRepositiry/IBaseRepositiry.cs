using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.Spcifications;

namespace Talabat.Core.IRepositiry
{
    public interface IBaseRepositiry<T> where T : BaseEntity
    {
        public Task<T> GetByIdAysnc(int id);
        
        public Task<IReadOnlyList<T>> GetAllAysnc();

        public Task<T> GetByIdSpacAysnc(ISpacification<T> spac);

        public Task<IReadOnlyList<T>> GetSpacAllAysnc(ISpacification<T> spac);

        public Task Update(T itme);

        public Task Delete(T item);

        public Task AddAysnc(T itme);

        public Task<int> GetCountWithSpac(ISpacification<T> spac);
    }
}
