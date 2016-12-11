namespace FlickrSearch.Logic.Infrastructure.Query
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}