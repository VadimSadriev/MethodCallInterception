using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProxyService.Proxy
{
    public interface IProxy<TService>
    {
        Task Execute(Expression<Func<TService, Task>> expression);

        Task<TResult> Execute<TResult>(Expression<Func<TService, Task<TResult>>> expression);

        Task Execute(Expression<Action<TService>> expression);

        Task<TResult> Execute<TResult>(Expression<Func<TService, TResult>> expression);
    }
}
