﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using webTemplate.Model;

namespace webTemplate.Model
{
    public partial class SqlRepository : IRepository
    {
        [Inject]
        public webTemplateDbDataContext Db { get; set; }

        public IQueryable<T> GetTable<T>() where T : class
        {
            return Db.GetTable<T>().AsQueryable<T>();
        }
    }
}