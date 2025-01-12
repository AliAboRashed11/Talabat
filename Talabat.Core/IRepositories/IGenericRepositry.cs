﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.IRepositories
{
   public interface IGenericRepositry <T>  where T : BaseEntity
    {

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetbyIdAsync(int id);
       

    }
}
