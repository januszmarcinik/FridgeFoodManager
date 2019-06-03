namespace FridgeFoodManager.Domain
{
    public interface IMediator
    {
        Result Command(ICommand command);

        T Query<T>(IQuery<T> query) where T : class;
    }
}
