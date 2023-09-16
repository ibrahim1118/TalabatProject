using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.Spcifications;

namespace Talabat.Repositiry.Spacification
{
    public static class SpacificationCreation<T> where T : BaseEntity
    {
        public static IQueryable<T> CreatSpacification(IQueryable<T> query ,ISpacification<T> spac) 
        {
            var quer = query;
            if (spac.Cariter is not null)
               quer= quer.Where(spac.Cariter); 

            if (spac.OrderBy is not null)
                quer= quer.OrderBy(spac.OrderBy);
            if (spac.OrderByDas is not null)
                 quer = quer.OrderByDescending(spac.OrderByDas);

            if (spac.IsPagnation)
                quer = quer.Skip(spac.Skip).Take(spac.Take);

            quer = spac.Includes.Aggregate(quer , (a ,b)=>a.Include(b));
            return quer; 

        }

    }
}
