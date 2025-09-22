namespace Example.Domain.Interfaces
{
    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }

    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
