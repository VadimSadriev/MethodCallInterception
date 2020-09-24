using Microsoft.Extensions.Logging;
using ProxyService.Infrastructure;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProxyService.Proxy
{
    public class Proxy<TService> : IProxy<TService>
    {
        private readonly TService _service;
        private readonly IValidator _validator;
        private readonly ILogger<Proxy<TService>> _logger;

        public Proxy(TService service, IValidator validator, ILogger<Proxy<TService>> logger)
        {
            _service = service;
            _validator = validator;
            _logger = logger;
        }

        protected virtual async Task OnEnter(ProxyMethodInfo info)
        {
            foreach (var arg in info.Args)
            {
                await _validator.ValidateAsync(arg);
            }
        }

        protected virtual async Task OnMethodExecuted(ProxyMethodInfo info, object? methodResult = null)
        {

        }

        protected virtual async Task OnException(Exception ex, ProxyMethodInfo info)
        {
            _logger.LogError(ex, "Execution of method {methodName} failed with args: {methodArgs}", info.Name, info.Args);
        }

        public async Task Execute(Expression<Func<TService, Task>> expression)
        {
            var methodInfo = GetMethodInfo(expression.Body);

            await OnEnter(methodInfo);

            try
            {
                await expression.Compile()(_service);
            }
            catch (Exception ex)
            {
                await OnException(ex, methodInfo);
                throw;
            }
            await OnMethodExecuted(methodInfo);
        }

        public async Task<TResult> Execute<TResult>(Expression<Func<TService, Task<TResult>>> expression)
        {
            var methodInfo = GetMethodInfo(expression.Body);

            await OnEnter(methodInfo);

            var result = default(TResult);

            try
            {
                result = await expression.Compile()(_service);
            }
            catch (Exception ex)
            {
                await OnException(ex, methodInfo);
                throw;
            }
            await OnMethodExecuted(methodInfo, result);
            return result;
        }

        public async Task Execute(Expression<Action<TService>> expression)
        {
            var methodInfo = GetMethodInfo(expression.Body);

            await OnEnter(methodInfo);

            try
            {
                expression.Compile()(_service);
            }
            catch (Exception ex)
            {
                await OnException(ex, methodInfo);
                throw;
            }
            await OnMethodExecuted(methodInfo);
        }

        public async Task<TResult> Execute<TResult>(Expression<Func<TService, TResult>> expression)
        {
            var methodInfo = GetMethodInfo(expression.Body);

            await OnEnter(methodInfo);

            var result = default(TResult);

            try
            {
                result = expression.Compile()(_service);
            }
            catch (Exception ex)
            {
                await OnException(ex, methodInfo);
                throw;
            }
            await OnMethodExecuted(methodInfo, result);
            return result;
        }

        private ProxyMethodInfo GetMethodInfo(Expression expression)
        {
            if (!(expression is MethodCallExpression methodCallExpression))
                throw new ArgumentException(nameof(expression), $"In order to get expression arguments expression has to be {nameof(MethodCallExpression)}");

            var args = new object[methodCallExpression.Arguments.Count];

            for (var i = 0; i < methodCallExpression.Arguments.Count; i++)
            {
                var arg = methodCallExpression.Arguments[i];

                var lambda = Expression.Lambda(Expression.Convert(arg, arg.Type));
                args[i] = lambda.Compile().DynamicInvoke();
            }

            return new ProxyMethodInfo(methodCallExpression.Method.Name, args);
        }
    }
}
