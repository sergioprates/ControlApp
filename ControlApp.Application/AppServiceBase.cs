﻿using ControlApp.Application.Interfaces;
using ControlApp.Dominio.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace ControlApp.Application
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity>
        where TEntity : class
    {

        private readonly IServiceBase<TEntity> _service;

        public AppServiceBase(IServiceBase<TEntity> service)
        {
            _service = service;
        }

        public void Add(TEntity obj)
        {
            _service.Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _service.GetAll();
        }

        public IEnumerable<TEntity> GetAllActive()
        {
            return _service.GetAllActive();
        }
    }
}
