using Castle.DynamicProxy;

namespace RW.PMS.CrossCutting.AOP.Interceptors
{
    public class DatabaseTransaction : IInterceptor
    {
        private readonly DatabaseTransactionAsync _databaseTransactionAsync;

        public DatabaseTransaction(DatabaseTransactionAsync databaseTransactionAsync)
        {
            _databaseTransactionAsync = databaseTransactionAsync;
        }

        public void Intercept(IInvocation invocation)
        {
            _databaseTransactionAsync.ToInterceptor().Intercept(invocation);
        }
    }
}