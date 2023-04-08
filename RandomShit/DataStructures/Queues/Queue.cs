namespace RandomShit.Queues;

/// <summary>
/// A queue implemented using an Linked List Approach
/// </summary>
/// <typeparam name="T">The Type of Data stored and returned by the Queue</typeparam>
public class Queue<T> : IQueue<T>
{
    private QueueNode<T>? _head, _tail;
    
    public void Add(T item)
    {
        var node = new QueueNode<T>(item);
        
        if (_head is null)
        {
            _head = node;
            _tail = node;
        }
        else
        {
            _head.Next = node;
            _head = node;
        }
    }

    public T? Get()
    {
        if (_tail is null) return default;

        var next = _tail.Next;
        var data = _tail.Data;
        _tail = next;
        return data;
    }

    public T? Peek()
    {
        return _tail is null ? default : _tail.Data;
    }
}

internal class QueueNode<T>
{
    public readonly T Data;
    public QueueNode<T>? Next { get; set; }

    public QueueNode(T data)
    {
        Data = data;
    }
}