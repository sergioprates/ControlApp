﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Dominio.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class
    {
        void Add(TEntity obj);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllActive();
    }
}
