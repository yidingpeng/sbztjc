using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using FreeSql;
using Microsoft.Extensions.Logging;
using RW.PMS.CrossCutting.AOP.Attributes;

namespace RW.PMS.CrossCutting.AOP.Interceptors
{
    public class DatabaseTransactionAsync : IAsyncInterceptor
    {
        private readonly UnitOfWorkManager _unitOfWorkManager;
        private IUnitOfWork _unitOfWork;

        public DatabaseTransactionAsync(UnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        ///     拦截同步方法
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptSynchronous(IInvocation invocation)
        {
            if (TryBegin(invocation))
                try
                {
                    invocation.Proceed();
                    _unitOfWork.Commit();
                }
                catch
                {
                    _unitOfWork.Rollback();
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            else
                invocation.Proceed();
        }

        /// <summary>
        ///     拦截异步方法
        /// </summary>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        /// <summary>
        ///     拦截带返回值的异步方法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="invocation"></param>
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            if (TryBegin(invocation))
                try
                {
                    invocation.Proceed();
                    if (invocation.ReturnValue != null) await (Task) invocation.ReturnValue;
                    _unitOfWork.Commit();
                }
                catch
                {
                    _unitOfWork.Rollback();
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            else
                invocation.Proceed();
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            TResult result;
            if (TryBegin(invocation))
            {
                try
                {
                    invocation.Proceed();
                    result = await (Task<TResult>) invocation.ReturnValue;
                    _unitOfWork.Commit();
                }
                catch
                {
                    _unitOfWork.Rollback();
                    throw;
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
            else
            {
                invocation.Proceed();
                result = await (Task<TResult>) invocation.ReturnValue;
            }

            return result;
        }

        private bool TryBegin(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var attribute = method.GetCustomAttributes(typeof(TransactionAttribute), false).FirstOrDefault();
            if (attribute is TransactionAttribute transaction)
            {
                _unitOfWork = _unitOfWorkManager.Begin(transaction.Propagation, transaction.IsolationLevel);
                return true;
            }

            return false;
        }
    }
}