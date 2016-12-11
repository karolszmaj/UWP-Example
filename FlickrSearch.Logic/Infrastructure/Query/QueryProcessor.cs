using System;
using System.Linq;
using System.Reflection;

namespace FlickrSearch.Logic.Infrastructure.Query
{
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly GetInstance getInstance;

        public QueryProcessor(GetInstance getInstance)
        {
            this.getInstance = getInstance;
        }

        public TResult Process<TResult>(IQuery<TResult> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            Type handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            object handler = this.getInstance(handlerType);
            var methodHandler = handler.GetType().GetRuntimeMethods().FirstOrDefault(x => x.Name == "Handle");

            try
            {
                return (TResult)methodHandler.Invoke(handler, new object[] { query });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Processing query failed", ex);
            }
        }
    }
}