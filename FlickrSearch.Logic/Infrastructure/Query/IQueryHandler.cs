namespace FlickrSearch.Logic.Infrastructure.Query
{
    public interface IQueryHandler<in TQuery, out TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}