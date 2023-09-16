﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;

namespace Talabat.Core.Spcifications
{
    public interface ISpacification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Cariter { get; set; }

        public List<Expression<Func<T ,object>>> Includes { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDas { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }  
        
        public bool IsPagnation { get; set; }

       

    }
}
