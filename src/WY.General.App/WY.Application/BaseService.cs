using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Common.IocTags;
using WY.EntityFramework.Repositories;

namespace WY.Application
{
    public interface IBaseService : IIocTransientTag { }

    public class BaseService<T, TPrimaryKey> : IBaseService where T : class
    {
        protected IRepository<T, TPrimaryKey> Repository { get; }

        public BaseService(IRepository<T, TPrimaryKey> repository)
        {
            Repository = repository;
        }
    }
}
