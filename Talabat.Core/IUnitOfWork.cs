using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.IRepositiry;

namespace Talabat.Core
{
    public interface IUnitOfWork :IAsyncDisposable
    {

        public IBaseRepositiry<T> Repostitry<T>() where T : BaseEntity;

        public Task<int> IsComplet(); 
    }
}
