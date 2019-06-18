public interface IConsumer<TResource>
{
    void Consume(TResource resource);
}
