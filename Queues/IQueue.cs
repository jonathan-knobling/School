namespace School.Queues;

public interface IQueue<T>
{
    T? Peek();
    T? Get();
    void Add(T item);
}